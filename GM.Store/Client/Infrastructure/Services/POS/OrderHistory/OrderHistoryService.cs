using GM.Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services.POS.Product
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly HttpClient http;
        private readonly IDataManager _dataManager;
        public OrderHistoryService(HttpClient http, IDataManager dataManager)
        {
            this.http = http;
            _dataManager = dataManager;
        }
        public async Task<ResponseData<List<OrderHistoryResponse>>> GetAllOrders(OrderHistoryRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseData<List<OrderHistoryResponse>>>(model, "api/order/history");
            return response;
        }
        public async Task<ResponseData<int>> GetOrderHistoryCount(OrderHistoryRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseData<int>>(model, "api/order/historycount");
            return response;
        }
        public async Task<TModel> GetOrderDetailAsync<TModel>(string localOrderId)
        where TModel : class
        => await this._dataManager.GetAsync<TModel>("/api/order/detail/" + localOrderId);
        public async Task<TModel> GetOrderDetailByIdAsync<TModel>(int orderId)
        where TModel : class
        => await this._dataManager.GetAsync<TModel>("/api/order/details/" + orderId);
    }
}
