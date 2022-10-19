using GM.Store.Server.Database;
using GM.Store.Server.Helper;
using GM.Store.Server.Models;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly ILogger<SmsController> _log;
        private readonly IDataAccessManager _dataAccessManager;
        private readonly ISmsHelper _smHelper;
        public SmsController(ILogger<SmsController> log, HttpClient http, IDataAccessManager dataAccessManager, ISmsHelper smHelper)
        {
            _log = log;
            _http = http;
            _dataAccessManager = dataAccessManager;
            _smHelper = smHelper;
        }
        [Route("Send")]
        [HttpPost]
        public async Task<IActionResult> Send(SmsRequestModel model)
        {
            try
            {
                var notify = new List<NotificationItem>();
                notify.Add(new NotificationItem
                {
                   
                    PhoneNumber = model.PhoneNumber,
                    Message = model.Message,
                });

                await _smHelper.SendAsync(notify, null, "BLKSMS", 0, "CITYMAPIA");
                return BadRequest(new ResponseData<bool> { Status = StatusCodes.Status500InternalServerError, Message = "Error" });

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest(new ResponseData<List<SyncModel>> {  Status = StatusCodes.Status500InternalServerError, Message = "Error" });
        }

        [Route("Log")]
        [HttpGet]
        public async Task<IActionResult> LogAsync()
        {
            try
            {
                return BadRequest(new ResponseData<bool> { Model = false, Status = StatusCodes.Status500InternalServerError, Message = "Error" });

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest(new ResponseData<bool> { Model = false, Status = StatusCodes.Status500InternalServerError, Message = "Error" });
        }


        [Route("template")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await _dataAccessManager.GetSmsTemplateAsync("api/sms/template", "Template");
                return Ok(new ResponseData<List<SmsTemplate>> { Model = response, Status = StatusCodes.Status200OK, Message = "Success" });
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }
    }
}
