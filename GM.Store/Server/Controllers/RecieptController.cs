using GM.Store.Server.Database;
using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GM.Store.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecieptController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly ILogger<RecieptController> _log;
        private readonly IDataAccessManager _dataAccessManager;
        public RecieptController(ILogger<RecieptController> log, HttpClient http, IDataAccessManager dataAccessManager)
        {
            _log = log;
            _http = http;
            _dataAccessManager = dataAccessManager;
        }

        [Route("All")]
        [HttpPost]
        public async Task<IActionResult> GetAll(ReceiptRequestModel model)
        {
            try
            {
                _http.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
                var response = await _dataAccessManager.GetAllRecieptAsync(model, "api/products", "Reciept");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }
            return null;
        }

        [Route("Import")]
        [HttpPost]
        public async Task<ProductImportResponse> Import(UploadedFile uploadedFile)
        {
            var responseModel = new ProductImportResponse();
            string rootPath = $@"{Directory.GetCurrentDirectory()}\Assets\Reciept\Import", extension = Path.GetExtension(uploadedFile.FileName), fileNameWithoutExt = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.UtcNow);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            var fileName = string.Format("{0}{1}", fileNameWithoutExt, extension);
            string excelFilePath = Path.Combine(rootPath, fileName);
            var fs = System.IO.File.Create(excelFilePath);
            fs.Write(uploadedFile.FileContent, 0, uploadedFile.FileContent.Length);
            fs.Close();
            int i = 1;
            string mapperFileParth = $@"{Directory.GetCurrentDirectory()}\Assets\ImportMappers\RecieptColumnMapper.xml";
            try
            {
                await new ImportV1.Import().ImportLargeFile(excelFilePath, mapperFileParth, async (importData, message, isSuccess) =>
                {
                    if (importData != null)
                    {
                        var item = new ReceiptModel
                        {
                            SLNO = importData.ContainsKey("SLNO") ? importData["SLNO"].ToString() : null,
                            RecieptNo = importData.ContainsKey("ReceiptNo") ? importData["ReceiptNo"].ToString() : null,
                            CustomerName = importData.ContainsKey("CustomerName") ? importData["CustomerName"].ToString() : null,
                            PhoneNumber = importData.ContainsKey("Telephone") ? importData["Telephone"].ToString() : null,
                            Brand = importData.ContainsKey("Brand") ? importData["Brand"].ToString() : null,
                            Model = importData.ContainsKey("Model") ? importData["Model"].ToString() : null,
                            Complaint = importData.ContainsKey("Complaint") ? importData["Complaint"].ToString() : null,
                            DeliveryDate = importData.ContainsKey("DeliveryDate") ? importData["DeliveryDate"].ToString() : null,
                            RecieptDate = importData.ContainsKey("ReceiptDate") ? importData["ReceiptDate"].ToString() : null
                        };
                        if (item != null && item.PhoneNumber != null)
                        {
                            var response = _dataAccessManager.ImportRecieptAsync(item);
                            if (response)
                                responseModel.SuccessCount += 1;
                            else
                                responseModel.FailedCount += 1;
                        }
                        else
                            responseModel.FailedCount += 1;
                        i++;
                        return 1;
                    }
                    return 0;
                });
                responseModel.IsProcessComplete = true;
                return responseModel;
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                responseModel.IsProcessComplete = true;
                return responseModel;
            }
        }

       
    }
}
