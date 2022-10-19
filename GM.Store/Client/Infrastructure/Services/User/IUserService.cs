using GM.Store.Shared.Models;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface IUserService
    {
        Task<TModel> GetProfileAsync<TModel>() where TModel : class;
        Task<ResponseData<UserResponseModel>> UpdateProfileAsync(UserRequestModel model);
        Task<ResponseData<UserOrderResponseModel>> OrderHistoryAsync(OrderRequestModel model);
        Task<ResponseData<UserOrderResponseModel>> CancelOrderHistoryAsync(OrderRequestModel model);
        Task<ResponseData<UserOrderResponseModel>> RefundedOrderHistoryAsync(OrderRequestModel model);
        Task<TModel> GetAppUsersAsync<TModel>(string role) where TModel : class;
        Task<TModel> GetDefaultSupplierAsync<TModel>() where TModel : class;
    }
}
