using GM.Store.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public class SyncService : ISyncService
    {
        private readonly IDataManager _dataManager;
        public SyncService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public async Task<TModel> GetAllItemsToSyncAsync<TModel>() where TModel : class
        {
            return await this._dataManager.GetAsync<TModel>($"api/sync");
        }
        public async Task<TModel> SyncAsync<TModel>(int tabId) where TModel : class
        {
            return await this._dataManager.GetAsync<TModel>($"api/sync/{tabId}");
        }
    }
}
