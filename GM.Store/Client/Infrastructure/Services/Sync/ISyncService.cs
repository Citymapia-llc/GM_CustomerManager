using GM.Store.Shared.Models;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public interface ISyncService
    {
        Task<TModel> GetAllItemsToSyncAsync<TModel>() where TModel : class;
        Task<TModel> SyncAsync<TModel>(int tabId) where TModel : class;
    }
}
