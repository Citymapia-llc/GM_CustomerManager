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
        public async Task<ResponseData<bool>> Send(SmsRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<bool>>(model, "api/sms/send");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
