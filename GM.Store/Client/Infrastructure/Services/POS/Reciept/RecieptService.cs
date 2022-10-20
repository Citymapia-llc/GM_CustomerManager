using GM.Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<ResponseListData<ReceiptModel>> GetLog(ReceiptRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseListData<ReceiptModel>>(model, "api/Reciept/log");
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
