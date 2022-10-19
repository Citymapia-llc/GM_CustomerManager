using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GM.Store.Client.Data;

namespace GM.Store.Client.Infrastructure.Services
{
    public class BusinessService :IBusinessService
    {
        private readonly HttpClient http;

        private const string ListPath = "/";
        public BusinessModel StoreDetails { get; set; }
        public TimeSpan? UserTimeSpan { get; set; }
        public string CurrentPortal { get; set; }
        public bool IsListEnabled { get; set; }
        [Inject]
        public NavigationManager MyNavigationManager { get; set; }
        public IDataManager _dtManager { get; set; }

      //  private readonly ISqliteWasmDbContextFactory<AppDbContext> _db;
        public BusinessService(HttpClient http, IDataManager dtManager)
        {
            this.http = http;
            _dtManager=dtManager;
         //   _db = db;
        }

        public async Task<TModel> GetAllAsync<TModel>()
            where TModel : class
        {
            try
            {
                //return await _localStore.GetAsync<TModel>("localedits", "business");
                // return  await this.http.GetFromJsonAsync<TModel>("api/business/settings");
                 return  await _dtManager.GetAsync<TModel>("api/business/settings");
            }
            catch (Exception)
            {
                return null;
                //return await _localStore.GetAsync<TModel>("business", null);
            }
            //using var ctx =await _db.CreateDbContextAsync();
            //this.http.DefaultRequestHeaders.Add("Referer", MyNavigationManager.BaseUri);

           //var m= await this.http.GetFromJsonAsync<TModel>("settings");

            //ctx.Application.Add(new Store.Shared.Application
            //{
            //    ApplicationRelativeUrl = "test"
            //});
            //await ctx.SaveChangesAsync();
            //return m;
            return null;
        }
        public async Task<TModel> GetCustomStyleAsync<TModel>()
           where TModel : class
        {
            return await this.http.GetFromJsonAsync<TModel>("settings/style");
        } 
        public async Task<TModel> GetListAsync<TModel>()
           where TModel : class
        {
            return await this._dtManager.GetAsync<TModel>("api/business/List");
        }
        public async Task<ResponseData<List<BusinessTimingModel>>> BusinessTimingAsyc(BusinessTimingRequest model)
        {
            try
            {
                var response = await this.http.PostAsJsonAsync("/business/businesstiming", model);
                var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<List<BusinessTimingModel>>>();
                return responseObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
