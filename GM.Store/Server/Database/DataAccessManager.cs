
using GM.Store.Client.Enums;
using GM.Store.Server.Config;
using GM.Store.Server.Helper;
using GM.Store.Server.Models;
using GM.Store.Server.Provider;
using GM.Store.Shared.Models;
using Newtonsoft.Json;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace GM.Store.Server.Database
{
    public interface IDataAccessManager
    {
        Task<ResponseData<BusinessModel>> BusinessSettingAsync(string apiUrl, string key = "");
        Task<ResponseData<Sync>> GetLastSyncTimeAsync(string apiUrl);
        Task<bool> GetAppTeamUsersAsync<AppTeamUser>(string apiUrl, string key = "");
        Task<ResponseData<AdminLoginResponseModel>> AdminLoginAsync(LoginModel req, string apiUrl, string key = "");
        Task<bool> RemoveBusinessSettingAsync(string key = "");
        Task<ResponseData<bool>> SyncProductsAsync(string apiUrl, string key = "");
        Task<ResponseData<bool>> SyncCategoriesAsync(string apiUrl, string key = "");
        Task<ResponseData<bool>> SyncSectionAsync(string apiUrl, string key = "");
        Task<ResponseData<bool>> SyncAdminUserAsync(string apiUrl, string key = "");
        Task<ResponseData<bool>> SyncChefListAsync(string apiUrl, string key = "");
        Task<ResponseData<List<SyncModel>>> GetOrAddSyncItemsAsync();
        bool ImportRecieptAsync(ReceiptModel req);
        Task<ResponseListData<Reciept>> GetAllRecieptAsync(ReceiptRequestModel model, string apiUrl, string key = "");
        Task<List<SmsTemplate>> GetSmsTemplateAsync(string apiUrl, string key = "");
    }

    /// <summary>
    /// Raven DB is used an an intermediate data store
    /// Ref: https://ravendb.net/docs/article-page/5.3/csharp/client-api/session/storing-entities
    /// </summary>
    public class DataAccessManager : IDataAccessManager, IDisposable
    {

        private readonly IDocumentStoreHolder _store;
        private readonly ILogger<DataAccessManager> _log;
        private readonly HttpClient _httpClient;
        private readonly IAsyncDocumentSession _session;
        private readonly ServiceConfiguration _serviceConfig;
        private readonly IJwtAuthProvider _jwtAuth;
        private readonly IEncodePasswordHelper _passwordHelper;

        public DataAccessManager(IDocumentStoreHolder store, HttpClient httpClient, ServiceConfiguration serviceConfig, IJwtAuthProvider jwtAuth,
            ILogger<DataAccessManager> log, IEncodePasswordHelper passwordHelper)
        {
            _httpClient = httpClient;
            _store = store;
            _serviceConfig = serviceConfig;
            _session = _store.Store.OpenAsyncSession();
            _jwtAuth = jwtAuth;
            _log = log;
            _passwordHelper = passwordHelper;
        }

        public async Task<ResponseData<BusinessModel>> BusinessSettingAsync(string apiUrl, string key = "")
        {
            ResponseData<BusinessModel>? model = default;
            try
            {
                using (var session = _store.Store.OpenAsyncSession())
                {
                    model = await session.LoadAsync<ResponseData<BusinessModel>>(key);
                    if (model == null)
                    {
                        model = await _httpClient.GetFromJsonAsync<ResponseData<BusinessModel>>(apiUrl);
                        if (model != null)
                        {
                            if (model.Model.EnableStore)
                            {
                                var settings = await session.Query<Settings>().FirstOrDefaultAsync();
                                if (settings == null)
                                {
                                    await session.StoreAsync(new Settings { BusinessExpiry = DateTime.UtcNow });
                                }
                                else if (settings.BusinessExpiry == DateTime.MinValue)
                                {
                                    settings.BusinessExpiry = DateTime.UtcNow.AddDays(5);
                                }
                                await session.StoreAsync(model, key);
                            }
                            else
                            {
                                return new ResponseData<BusinessModel> { Model = null, Status = StatusCodes.Status503ServiceUnavailable };
                            }
                        }
                    }
                    else
                    {
                        var settings = await session.Query<Settings>().FirstOrDefaultAsync();
                        if (settings == null)
                        {
                            await session.StoreAsync(new Settings { BusinessExpiry = DateTime.UtcNow });
                        }
                        else if (settings.BusinessExpiry == DateTime.MinValue)
                        {
                            settings.BusinessExpiry = DateTime.UtcNow.AddDays(5);
                        }
                    }
                    await session.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                Console.WriteLine(ex.Message);
            }
            return model;
        }

       

        public async Task<ResponseData<Sync>> GetLastSyncTimeAsync(string apiUrl)
        {

            ResponseData<Sync>? respModel = new ResponseData<Sync>();
            respModel.Status = StatusCodes.Status500InternalServerError;
            try
            {

                respModel.Model = new Sync();
                _httpClient.DefaultRequestHeaders.Add("ApplicationSecret", _serviceConfig.ApplicationSecret);
                var syncTime = await DataSync.GetLastSyncTimeAsync(_store, _httpClient, 1, apiUrl);
                respModel.Model.LastUpdatedTime = syncTime;
                respModel.Status = StatusCodes.Status200OK;
                return respModel;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return respModel;
        }

        public async Task<bool> GetAppTeamUsersAsync<AppTeamUser>(string apiUrl, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = apiUrl;

            List<AppTeamUser> model = default;
            try
            {
                using (var session = _store.Store.OpenAsyncSession())
                {
                    model = await session.Query<AppTeamUser>().ToListAsync();

                }
                if (model.Count() == 0)
                {
                    var respModel = await _httpClient.GetFromJsonAsync<ResponseData<List<AppTeamUser>>>(apiUrl);
                    if (respModel != null && respModel.Succeeded)
                    {
                        using (var session = _store.Store.OpenAsyncSession())
                        {
                            foreach (var item in respModel.Model)
                            {
                                await session.StoreAsync(item);
                            }
                            await session.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);

                Console.WriteLine(ex);
                return false;
            }

        }
        public async Task<ResponseData<List<SyncModel>>> GetOrAddSyncItemsAsync()
        {
            ResponseData<List<SyncModel>> resp = new ResponseData<List<SyncModel>>();
            try
            {
                using (var session = _store.Store.OpenAsyncSession())
                {
                    resp.Model = await session.Query<SyncModel>().ToListAsync();
                }
                if (resp.Model.Count() == 0)
                {
                    using (var session = _store.Store.OpenAsyncSession())
                    {
                        resp.Model.Add(new SyncModel { ItemId = 1, Name = "Categories", CreatedOn = DateTime.UtcNow });
                        resp.Model.Add(new SyncModel { ItemId = 2, Name = "Items", CreatedOn = DateTime.UtcNow });
                        resp.Model.Add(new SyncModel { ItemId = 3, Name = "Section", CreatedOn = DateTime.UtcNow });
                        resp.Model.Add(new SyncModel { ItemId = 4, Name = "Admin Users", CreatedOn = DateTime.UtcNow });
                        resp.Model.Add(new SyncModel { ItemId = 5, Name = "Chef List", CreatedOn = DateTime.UtcNow });
                        foreach (var item in resp.Model)
                        {
                            await session.StoreAsync(item);
                        }
                        await session.SaveChangesAsync();
                    }
                }
                resp.Status = StatusCodes.Status200OK;
                return resp;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);

                Console.WriteLine(ex);
                return resp;
            }

        }
        public async Task<ResponseData<AdminLoginResponseModel>> AdminLoginAsync(LoginModel req, string apiUrl, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = apiUrl;

            ResponseData<AdminLoginResponseModel> model = new ResponseData<AdminLoginResponseModel>();
            model.Status = StatusCodes.Status500InternalServerError;
            List<AppTeamUser> respModel = default;
            try
            {
                using (var session = _store.Store.OpenAsyncSession())
                {
                    respModel = await session.Query<AppTeamUser>().ToListAsync();

                }
                if (respModel.Count() == 0)
                {
                    var response = await this._httpClient.PostAsJsonAsync(apiUrl, req);
                    model = await response.Content.ReadFromJsonAsync<ResponseData<AdminLoginResponseModel>>();
                }
                else
                {
                    var resp = respModel.Where(a => a.Email == req.UserName).FirstOrDefault();
                    if (resp != null && req.Password != null && resp.Code != null)
                    {
                        var hashCode = resp.Code;
                        var password = _passwordHelper.EncodePassword(req.Password, hashCode);
                        if (resp.PasswordHash == password)
                        {
                            var roles = string.Join(",", resp.Role);
                            var authClaims = new[]
                               {
                                new Claim(ClaimTypes.Name, resp.Email),
                                new Claim("UserId", resp.UserId.ToString()),
                                new Claim("FullName", resp.FirstName),
                                new Claim("UserName", resp.Email),
                                new Claim("TokenType", "bearer"),
                                new Claim("Roles", roles),
                                new Claim("AppId", resp.ApplicationId.ToString()),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };
                            var jwtToken = _jwtAuth.GenerateTokens(resp.Email, authClaims, DateTime.UtcNow);
                            model = new ResponseData<AdminLoginResponseModel>
                            {

                                Model = new AdminLoginResponseModel
                                {
                                    AuthToken = jwtToken,
                                    UserRoles = new List<string> { resp.Role }
                                },
                                Message = "success",
                                Status = 200
                            };
                            return model;
                        }
                        model = new ResponseData<AdminLoginResponseModel>
                        {
                            Message = "Incorrect Password",
                            Status = 500
                        };
                        return model;
                    }
                    model = new ResponseData<AdminLoginResponseModel>
                    {
                        Message = "User is not a valid Team Member!",
                        Status = 400
                    };
                    return model;
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);

                Console.WriteLine(ex);
            }
            return model;
        }

        public async Task<ResponseData<bool>> SyncProductsAsync(string apiUrl, string key = "")
        {

            ResponseData<bool> respModel = new ResponseData<bool>();
            respModel.Status = StatusCodes.Status500InternalServerError;
            try
            {
                if (string.IsNullOrEmpty(key))
                    key = apiUrl;


                var response = await _httpClient.PostAsJsonAsync(apiUrl, new ProductRequestModel { CurrentPage = 1, PageSize = 300 });
                var responseModel = await response.Content.ReadFromJsonAsync<ResponseListData<Product>>();
                if (responseModel != null && responseModel.Succeeded)
                {
                    using (var session = _store.Store.OpenAsyncSession())
                    {
                        var list = await session.Query<Product>().ToListAsync();
                        foreach (var item in list)
                        {
                            session.Delete<Product>(item);
                        }
                        await session.SaveChangesAsync();
                        foreach (var item in responseModel.Model.List)
                        {
                            await session.StoreAsync(item);
                        }
                        var syncItem = await session.Query<SyncModel>().Where(a => a.ItemId == 2).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            syncItem.LastSyncTime = DateTime.Now;
                        }
                        await session.SaveChangesAsync();
                    }
                    respModel.Model = true;
                    respModel.Status = StatusCodes.Status200OK;
                }
                return respModel;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return respModel;
        }
        public async Task<ResponseData<bool>> SyncCategoriesAsync(string apiUrl, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = apiUrl;

            ResponseData<bool> respModel = new ResponseData<bool>();
            respModel.Status = StatusCodes.Status500InternalServerError;
            try
            {
                var model = await _httpClient.GetFromJsonAsync<ResponseData<List<FeaturedCategory>>>(apiUrl);
                if (model != null)
                {

                    using (var session = _store.Store.OpenAsyncSession())
                    {
                        var list = await session.LoadAsync<ResponseData<List<FeaturedCategory>>>(key);
                        session.Delete(list);
                        await session.SaveChangesAsync();

                        await session.StoreAsync(model, key);
                        var syncItem = await session.Query<SyncModel>().Where(a => a.ItemId == 1).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            syncItem.LastSyncTime = DateTime.Now;
                        }
                        await session.SaveChangesAsync();
                    }
                    respModel.Model = true;
                    respModel.Status = StatusCodes.Status200OK;
                }
                return respModel;
            }
            catch (Exception ex)
            {

                _log.LogError(ex, ex.Message);
            }
            return respModel;

        }

        public async Task<ResponseData<bool>> SyncSectionAsync(string apiUrl, string key = "")
        {
            ResponseData<bool>? respModel = new ResponseData<bool>();
            respModel.Status = StatusCodes.Status500InternalServerError;
            try
            {
                if (string.IsNullOrEmpty(key))
                    key = apiUrl;


                var response = await _httpClient.GetFromJsonAsync<ResponseData<List<Section>>>(apiUrl);
                if (response != null && response.Succeeded)
                {
                    using (var session = _store.Store.OpenAsyncSession())
                    {
                        foreach (var item in response.Model)
                        {
                            await session.StoreAsync(item);
                        }
                        var syncItem = await session.Query<SyncModel>().Where(a => a.ItemId == 3).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            syncItem.LastSyncTime = DateTime.Now;
                        }
                        await session.SaveChangesAsync();
                    }
                    respModel.Status = StatusCodes.Status200OK;
                    respModel.Model = true;
                }
                return respModel;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return respModel;
        }

        public async Task<ResponseData<bool>> SyncAdminUserAsync(string apiUrl, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = apiUrl;

            ResponseData<bool> model = new ResponseData<bool>();
            model.Status = StatusCodes.Status500InternalServerError;
            try
            {
                var respModel = await _httpClient.GetFromJsonAsync<ResponseData<List<AppTeamUser>>>(apiUrl);
                if (respModel != null && respModel.Succeeded)
                {
                    using (var session = _store.Store.OpenAsyncSession())
                    {
                        var userList = await session.Query<AppTeamUser>().ToListAsync();
                        foreach (var item in userList)
                        {
                            session.Delete(item);
                        }
                        await session.SaveChangesAsync();
                        foreach (var item in respModel.Model)
                        {
                            await session.StoreAsync(item);
                        }
                        var syncItem = await session.Query<SyncModel>().Where(a => a.ItemId == 4).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            syncItem.LastSyncTime = DateTime.Now;
                        }
                        await session.SaveChangesAsync();
                    }
                    model.Status = StatusCodes.Status200OK;
                    model.Model = true;
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);

                Console.WriteLine(ex);
            }
            return model;
        }

        public async Task<ResponseData<bool>> SyncChefListAsync(string apiUrl, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = apiUrl;

            ResponseData<bool> respModel = new ResponseData<bool>();
            respModel.Status = StatusCodes.Status500InternalServerError;
            try
            {
                var model = await _httpClient.GetFromJsonAsync<ResponseData<List<Chef>>>(apiUrl);
                if (model != null)
                {

                    using (var session = _store.Store.OpenAsyncSession())
                    {
                        var list = await session.LoadAsync<ResponseData<List<Chef>>>(key);
                        session.Delete(list);
                        await session.SaveChangesAsync();

                        await session.StoreAsync(model, key);

                        var syncItem = await session.Query<SyncModel>().Where(a => a.ItemId == 5).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            syncItem.LastSyncTime = DateTime.Now;
                        }
                        await session.SaveChangesAsync();

                    }
                    respModel.Model = true;
                    respModel.Status = StatusCodes.Status200OK;
                }
                return respModel;
            }
            catch (Exception ex)
            {

                _log.LogError(ex, ex.Message);
            }
            return respModel;

        }
        public void Dispose()
        {
            _session.Dispose();
        }
        public async Task<bool> RemoveBusinessSettingAsync(string key = "")
        {

            try
            {
                using (var session = _store.Store.OpenAsyncSession())
                {
                    var settings = await session.Query<Settings>().FirstOrDefaultAsync();
                    if (settings != null && settings.BusinessExpiry <= DateTime.UtcNow)
                    {
                        settings.BusinessExpiry = DateTime.MinValue;
                        var model = await session.LoadAsync<ResponseData<BusinessModel>>(key);
                        session.Delete(model);
                        await session.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public bool ImportRecieptAsync(ReceiptModel req)
        {
            try
            {
                using (var session = _store.Store.OpenSession())
                {
                    var objProduct = session.Query<Reciept>().Where(a => a.RecieptNo == req.RecieptNo).FirstOrDefault();
                    var localRecieptId = Guid.NewGuid().ToString("N").Substring(0, 6);
                    var item = new Reciept
                    {
                        LocalId = Guid.NewGuid().ToString("N").Substring(0, 6),
                        CustomerName = req.CustomerName,
                        RecieptNo = req.RecieptNo,
                        RecieptDate = req.RecieptDate,
                        PhoneNumber = req.PhoneNumber,
                        Brand = req.Brand,
                        Model = req.Model,
                        Complaint = req.Complaint,
                        DeliveryDate = req.DeliveryDate,
                        CreatedOn = DateTime.UtcNow,
                        LastUpdatedOn = DateTime.UtcNow,
                        IsToSync=true,
                        SLNO=req.SLNO,
                    };
                    if (objProduct == null)
                        session.Store(item, $"{item.LocalId}");
                    else
                    {
                        objProduct.CustomerName = item.CustomerName;
                        objProduct.PhoneNumber = item.PhoneNumber;
                        objProduct.Model = item.Model;
                        objProduct.Complaint = item.Complaint;
                        objProduct.DeliveryDate = item.DeliveryDate;
                        objProduct.RecieptDate = item.RecieptDate;
                        objProduct.LastUpdatedOn = item.LastUpdatedOn;
                        objProduct.IsToSync = item.IsToSync;
                    }
                    var user = session.Query<User>().Where(a => a.PhoneNumber.Trim() == req.PhoneNumber.Trim()).FirstOrDefault();
                    if(user == null)
                    {
                        var userObj = new User
                        {
                            LocalId = Guid.NewGuid().ToString("N").Substring(0, 6),
                            PhoneNumber = req.PhoneNumber,
                            UserName = req.CustomerName == null ? req.PhoneNumber : req.CustomerName,
                            IsToSync = true
                        };
                        session.Store(userObj, $"{item.LocalId}");
                    }

                    session.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return false;
        }

        public async Task<ResponseListData<Reciept>> GetAllRecieptAsync(ReceiptRequestModel model, string apiUrl, string key = "") 
        {

            ResponseListData<Reciept>? respModel = new ResponseListData<Reciept>();
            respModel.Status = StatusCodes.Status500InternalServerError;
            try
            {
                if (string.IsNullOrEmpty(key))
                    key = apiUrl;
                int totalCount = 0;
                int pendingSyncCount = 0;
                using (var session = _store.Store.OpenAsyncSession())
                {
                    var list = await session.Query<Reciept>().ToListAsync();

                    if (list.Count() == 0)
                    {
                        
                            return respModel;
                    }

                    if (list != null)
                    {
                        if (!string.IsNullOrEmpty(model.Keyword))
                        {
                            model.Keyword = model.Keyword.ToLower();
                            list = list.Where(x => (x.CustomerName != null && x.CustomerName.ToLower().Contains(model.Keyword)) || (x.PhoneNumber != null && x.PhoneNumber.ToLower().Contains(model.Keyword)) ||
                            (x.RecieptNo != null && x.RecieptNo.ToLower().Contains(model.Keyword)) ||(x.Brand != null && x.Brand.ToLower().Contains(model.Keyword)) || (x.Complaint != null && x.Complaint.ToLower().Contains(model.Keyword))).ToList();
                        }
                        totalCount = list.Count();
                        pendingSyncCount = list.Where(a => a.IsToSync == true).Count();
                        list = list.OrderByDescending(a => a.IsToSync).ThenByDescending(a => a.LastUpdatedOn).Skip(model.CurrentPage).Take(model.PageSize).ToList();
                        respModel.Model.List = list;
                        respModel.Model.Pager = new GMPager { TotalCount = totalCount, CurrentPage = model.CurrentPage, PageSize = model.PageSize, PendingSyncCount = pendingSyncCount };
                        respModel.Status = StatusCodes.Status200OK;
                    }
                }

                return respModel;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return respModel;
        }

        public async Task<List<SmsTemplate>> GetSmsTemplateAsync(string apiUrl, string key = "")
        {
            List<SmsTemplate>? respModel = new List<SmsTemplate>();
            try
            {
                if (string.IsNullOrEmpty(key))
                    key = apiUrl;
                using (var session = _store.Store.OpenAsyncSession())
                {
                    respModel = await session.Query<SmsTemplate>().ToListAsync();
                }
                if (respModel == null || respModel.Count() == 0)
                {
                    var respObj = await this._httpClient.GetFromJsonAsync<ResponseData<List<SmsTemplate>>>(apiUrl);
                    if (respObj != null && respObj.Succeeded)
                    {
                        using (var session = _store.Store.OpenAsyncSession())
                        {
                            foreach (var item in respObj.Model)
                            {
                                await session.StoreAsync(item, item.Id);
                            }
                            await session.SaveChangesAsync();

                            respModel = await session.Query<SmsTemplate>().ToListAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return respModel;
        }
    }
}
