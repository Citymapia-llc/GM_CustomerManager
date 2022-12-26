using GM.Store.Server.Database;
using GM.Store.Server.Helper;
using GM.Store.Server.Models;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static GM.Store.Client.Infrastructure.Services.RecieptService;
using System.Text;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly ILogger<SmsController> _log;
        private readonly IDataAccessManager _dataAccessManager;
        public SmsController(ILogger<SmsController> log, HttpClient http, IDataAccessManager dataAccessManager)
        {
            _log = log;
            _http = http;
            _dataAccessManager = dataAccessManager;
        }
        [Route("Send")]
        [HttpPost]
        public async Task<IActionResult> Send(SmsRequestModel model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var notify = new List<NotificationItem>();
                notify.Add(new NotificationItem
                {
                    PhoneNumber = model.PhoneNumber,
                    Message = model.Message,
                });
                var response = await _http.PostAsJsonAsync("api/sms/send", notify);
                var respModel = await response.Content.ReadFromJsonAsync<ResponseData<int>>();
                if (respModel != null && respModel.Succeeded)
                {
                    var receiptResponse = await _dataAccessManager.ConfirmRecieptAsync(model);
                    return Ok(new ResponseData<bool> { Status = StatusCodes.Status200OK, Message = "Success" });
                }
                return BadRequest(new ResponseData<List<SyncModel>> { Status = StatusCodes.Status500InternalServerError, Message = "Error" });

            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest(new ResponseData<List<SyncModel>> { Status = StatusCodes.Status500InternalServerError, Message = "Error" });
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
        [Route("balance")]
        [HttpGet]
        public async Task<IActionResult> GetSmsBalance()
        {
            try
            {
                model_messge post_values = new model_messge
                {

                    api_id = "APIcGRNf3Ht104272",

                    api_password = "uIyCcBVW",


                };
                using (var _httpClient = new HttpClient())
                {
                    var requestMessage = new HttpRequestMessage()
                    {
                        Method = new HttpMethod("POST"),
                        RequestUri = new Uri($"https://www.bulksmsplans.com/api/check_balance"),
                        Content = new StringContent(JsonConvert.SerializeObject(post_values), Encoding.UTF8, "application/json")
                    };
                    requestMessage.Content.Headers.TryAddWithoutValidation(
                        "x-custom-header", "value");

                    var response = await _httpClient.SendAsync(requestMessage);
                    var responseStatusCode = response.StatusCode;
                    if (responseStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var respModel = await response.Content.ReadFromJsonAsync<ResponseSmsData<List<SmsBalanceModel>>>();
                        return Ok(new ResponseSmsData<SmsBalanceModel> { data = respModel.data.FirstOrDefault(), Status = StatusCodes.Status200OK, Message = "Success" });
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return BadRequest();
        }
    }
}
