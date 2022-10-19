using GM.Store.Server.Models;
using GM.Store.Shared.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace GM.Store.Server.Database
{
    public class DataSync
    {
        internal static async Task<DateTime> GetLastSyncTimeAsync(IDocumentStoreHolder _store, HttpClient _httpClient, int appId, string apiUrl)
        {
            try
            {
                DateTime lastSyncTime = DateTime.UtcNow;
                var response = await _httpClient.GetFromJsonAsync<ResponseData<Sync>>(apiUrl);
                if (response != null && response.Succeeded)
                {
                    lastSyncTime = response.Model == null ? DateTime.UtcNow : response.Model.LastUpdatedTime;
                }
                return lastSyncTime;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return new DateTime();
        }

       
    }
}
