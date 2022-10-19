using GM.Store.Server.Config;
using GM.Store.Server.Database;
using GM.Store.Server.Models;
using GM.Store.Shared;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    { 
        private readonly HttpClient _http;
        private readonly ILogger<BusinessController> _log;
        private readonly ServiceConfiguration _serviceConfig;
       
        private readonly IDataAccessManager _dataAccessManager;

        public BusinessController(ILogger<BusinessController> log, HttpClient http,IDataAccessManager dataAccessManager, ServiceConfiguration serviceConfig)
        {
            _log = log;
            _http = http;
            _dataAccessManager = dataAccessManager;
            _serviceConfig = serviceConfig;
        }
        [Route("settings")]
        [HttpGet]
        public async Task<IActionResult> Setting()
        {
            try
            {
                _log.LogWarning("Initial business settings api call ", "");
                _http.DefaultRequestHeaders.Add("ApplicationSecret", _serviceConfig.ApplicationSecret);
                //_http.DefaultRequestHeaders.Add("Referer", Request.GetTypedHeaders().Referer.ToString());
                var model = await _dataAccessManager.BusinessSettingAsync("settings", "settings");
                if (model != null && model.Succeeded)
                {
                    _http.DefaultRequestHeaders.Add("Authorization",$"Bearer {model.Model.AuthToken}");
                    await _dataAccessManager.GetAppTeamUsersAsync<AppTeamUser>("/api/user/team", "teamusers");
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                Console.WriteLine(ex);
            }
            return null;
        }


    }

}
