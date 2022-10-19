using GM.Store.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly IDataManager _dataManager;
        public UserService(HttpClient http, IDataManager dataManager)
        {
            this.httpClient = http;
            _dataManager = dataManager;
        }
        public async Task<TModel> GetProfileAsync<TModel>() where TModel : class
        {
            return await this._dataManager.GetAsync<TModel>("api/user/myprofile");
        }
        public async Task<ResponseData<UserResponseModel>> UpdateProfileAsync(UserRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseData<UserResponseModel>>(model, "api/user/myprofile");
            return response;
        }
        public async Task<ResponseData<UserOrderResponseModel>> OrderHistoryAsync(OrderRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseData<UserOrderResponseModel>>(model, "api/user/userorderhistory");
            return response;
        }
        public async Task<ResponseData<UserOrderResponseModel>> CancelOrderHistoryAsync(OrderRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseData<UserOrderResponseModel>>(model, "api/user/usercancelledorders");
            return response;
        }
        public async Task<ResponseData<UserOrderResponseModel>> RefundedOrderHistoryAsync(OrderRequestModel model)
        {
            var response = await _dataManager.PostAsync<ResponseData<UserOrderResponseModel>>(model, "api/user/userrefundorders");
            return response;
        }

        public async Task<TModel> GetAppUsersAsync<TModel>(string role) where TModel : class
        {
            return await this._dataManager.GetAsync<TModel>("api/user/" + role);
        }
        public async Task<TModel> GetDefaultSupplierAsync<TModel>() where TModel : class
        {
            return await this._dataManager.GetAsync<TModel>("api/user/DefaultSupplier");
        }
    }
}
