using GM.Store.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GM.Store.Client.Infrastructure.Services.POS.Product
{
    public interface IOrderHistoryService
    {
        Task<ResponseData<List<OrderHistoryResponse>>> GetAllOrders(OrderHistoryRequestModel model);
        Task<ResponseData<int>> GetOrderHistoryCount(OrderHistoryRequestModel model);
        Task<TModel> GetOrderDetailAsync<TModel>(string localOrderId)
        where TModel : class;
        Task<TModel> GetOrderDetailByIdAsync<TModel>(int orderId)
        where TModel : class;

    }
}
