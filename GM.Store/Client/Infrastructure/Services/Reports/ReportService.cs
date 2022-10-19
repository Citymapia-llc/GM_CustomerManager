using GM.Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient http;

        private const string ListPath = "api/report";

        private readonly IDataManager _dataManager;
        public ReportService(HttpClient http, IDataManager dataManager)
        {
            this.http = http;
            _dataManager = dataManager;
        }
        public async Task<TModel> GetAllAsync<TModel>() where TModel : class
         => await this._dataManager.GetAsync<TModel>(ListPath+"/all");

        public async Task<ResponseData<DayEndReportModel>> GenerateDayEndReportAsync(ReportRequest model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<DayEndReportModel>>(model, ListPath + "/dayend");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseData<List<ItemSalesReportModel>>> GenerateItemSalesReportAsync(ReportRequest model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<List<ItemSalesReportModel>>>(model, ListPath + "/itemSales");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseData<List<ItemSalesReportModel>>> GenerateComplementaryItemReportAsync(ReportRequest model)
        {
            try
            {
               // var response = await this.http.PostAsJsonAsync(ListPath + "/complementaryitems", model);
                //var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<List<ItemSalesReportModel>>>();
                var response = await _dataManager.PostAsync<ResponseData<List<ItemSalesReportModel>>>(model, ListPath + "/complementaryitems");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseData<List<ItemSalesReportModel>>> GenerateVoidItemReportAsync(ReportRequest model)
        {
            try
            {
                //var response = await this.http.PostAsJsonAsync(ListPath + "/voiditems", model);
                //var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<List<ItemSalesReportModel>>>();
                //return responseObject;
                var response = await _dataManager.PostAsync<ResponseData<List<ItemSalesReportModel>>>(model, ListPath + "/voiditems");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseData<dynamic>> GenerateReportDataAsync(ReportRequest model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<dynamic>>(model, ListPath + "/generate");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
