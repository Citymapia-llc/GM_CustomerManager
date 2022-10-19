using GM.Store.Server.Database;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly ILogger<AccountController> _log;

        private readonly IDataAccessManager _dataAccessManager;
        public AccountController(ILogger<AccountController> log, HttpClient http, IDataAccessManager dataAccessManager)
        {
            _log = log;
            _http = http;
            _dataAccessManager = dataAccessManager;
        }
        [Route("adminlogin")]
        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginModel model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                // var response = await this._http.PostAsJsonAsync("Account/admin/login", model);
                //var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<AdminLoginResponseModel>>();
                var responseObject = await _dataAccessManager.AdminLoginAsync(model, "Account/admin/login", "");
                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }
    }
}
