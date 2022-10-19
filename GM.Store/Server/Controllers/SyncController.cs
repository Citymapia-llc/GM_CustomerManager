using GM.Store.Server.Database;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly ILogger<SyncController> _log;

        private readonly IDataAccessManager _dataAccessManager;
        public SyncController(ILogger<SyncController> log, HttpClient http, IDataAccessManager dataAccessManager)
        {
            _log = log;
            _http = http;
            _dataAccessManager = dataAccessManager;
        }
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllItemToSync()
        {
            try
            {
                var chefResp = await this._dataAccessManager.GetOrAddSyncItemsAsync();
                return Ok(chefResp);

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest(new ResponseData<List<SyncModel>> {  Status = StatusCodes.Status500InternalServerError, Message = "Error" });
        }

        [Route("{typeId}")]
        [HttpGet]
        public async Task<IActionResult> ItemSync(int typeId)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                switch (typeId)
                {
                    case 1:
                        var response = await this._dataAccessManager.SyncCategoriesAsync("api/products/treecategories", "categories");
                        return Ok(response);
                    case 2:
                        var productResp = await this._dataAccessManager.SyncProductsAsync("api/products", "items");
                        return Ok(productResp);
                    case 3:
                        var sectionResp = await this._dataAccessManager.SyncSectionAsync("api/table/GetSectionTables", "Sections");
                        return Ok(sectionResp);
                    case 4:
                        var adminUserResp = await this._dataAccessManager.SyncAdminUserAsync("/api/user/team", "teamusers");
                        return Ok(adminUserResp);
                    case 5:
                        var chefResp = await this._dataAccessManager.SyncChefListAsync("api/user/chef", "chef");
                        return Ok(chefResp);
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest(new ResponseData<bool> { Model = false, Status = StatusCodes.Status500InternalServerError, Message = "Error" });
        }
    }
}
