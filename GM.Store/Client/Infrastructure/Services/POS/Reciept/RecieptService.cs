using GM.Store.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public class RecieptService : IRecieptService
    {
        private const string ProductPath = "api/Reciept";

        private readonly IDataManager _dataManager;
        public RecieptService( IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<ResponseListData<ReceiptModel>> GetAll(ReceiptRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseListData<ReceiptModel>>(model, "api/Reciept/all");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class model_messge
        {

            public string api_id
            {

                get;

                set;

            }

            public string api_password
            {

                get;

                set;

            }

        }



        
        public async Task<ResponseSmsData<SmsBalanceModel>> CheckSmsBalanace()
        {
            try
            {
                var response = await _dataManager.GetAsync<ResponseSmsData<SmsBalanceModel>>("api/sms/balance");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseListData<ConfirmedReciept>> GetLog(ReceiptRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseListData<ConfirmedReciept>>(model, "api/Reciept/log");
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
