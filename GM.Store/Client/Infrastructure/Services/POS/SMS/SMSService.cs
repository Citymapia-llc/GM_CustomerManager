using GM.Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public class SMSService : ISMSService
    {
        private const string ProductPath = "api/sms";

        private readonly IDataManager _dataManager;
        public SMSService( IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<ResponseData<List<SmsTemplateModel>>> GetAllTempate()
        {
            try
            {
                var response = await _dataManager.GetAsync<ResponseData<List<SmsTemplateModel>>>("api/sms/template");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ProductImportResponse> Import(UploadedFile model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ProductImportResponse>(model, "api/Reciept/import");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
