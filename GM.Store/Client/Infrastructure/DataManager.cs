using GM.Store.Client.Data;
using GM.Store.Client.Infrastructure.Extensions;
using System.Net.Http.Json;

namespace GM.Store.Client.Infrastructure
{
    public interface IDataManager
    {
        Task<TModel> GetAsync<TModel>(string apiUrl);
        Task<TModel> PostAsync<TModel>(object model, string apiUrl);
    }
    public class DataManager:IDataManager
    {
        private readonly HttpClient _httpClient;
        public DataManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TModel> GetAsync<TModel>(string apiUrl)
        {
            try
            {
                //API
                return await _httpClient.GetFromJsonAsync<TModel>(apiUrl);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<TModel> PostAsync<TModel>(object model,string apiUrl)
        {
            try
            {
                var response = await this._httpClient.PostAsJsonAsync(apiUrl, model);
                var responseObject = await response.Content.ReadFromJsonAsync<TModel>();
                return responseObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
