using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly HttpClient _http;
        private readonly ILogger<ReportController> _log;
        public ReportController(ILogger<ReportController> log, HttpClient http)
        {
            _log = log;
            _http = http;
        }
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await this._http.GetFromJsonAsync<ResponseData<List<ReportsModel>>>("api/reports");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }

        [Route("dayend")]
        [HttpPost]
        public async Task<IActionResult> DayEndReport(ReportRequest model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await this._http.PostAsJsonAsync("api/reports/dayend", model);
                var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<DayEndReportModel>>();
                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }

        [Route("itemSales")]
        [HttpPost]
        public async Task<IActionResult> ItemSalesReport(ReportRequest model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await this._http.PostAsJsonAsync("api/reports/itemSales", model);
                var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<List<ItemSalesReportModel>>>();
                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }

        [Route("generate")]
        [HttpPost]
        public async Task<IActionResult> GenerateReportData(ReportRequest model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await this._http.PostAsJsonAsync("api/reports/generate", model);
                var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<dynamic>>();
                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }
        [Route("complementaryitems")]
        [HttpPost]
        public async Task<IActionResult> ComplementaryItemsReport(ReportRequest model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await this._http.PostAsJsonAsync("api/reports/complementaryitems", model);
                var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<List<ItemSalesReportModel>>>();
                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }
        [Route("voiditems")]
        [HttpPost]
        public async Task<IActionResult> VoidItemsReport(ReportRequest model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await this._http.PostAsJsonAsync("api/reports/voiditems", model);
                var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<List<ItemSalesReportModel>>>();
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
