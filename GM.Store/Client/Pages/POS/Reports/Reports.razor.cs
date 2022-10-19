using GM.Store.Client.Infrastructure.Helper;
using GM.Store.Shared.Models;
using Newtonsoft.Json;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Pages.POS.Reports
{
    public partial class Reports
    {
        private bool isLoading;
        private int ActiveReportId { get; set; }
        protected ReportRequest CustomReportRequest = new ReportRequest();
        protected ReportRequest reportRequest = new ReportRequest();
        protected BusinessModel businessModel;
        public List<ReportsModel> model { get; set; }
        public dynamic orderModel { get; set; }
        public DayEndReportModel DayEndReportModel { get; set; }
        public List<ItemSalesReportModel> ItemSalesReportModel { get; set; }
        List<Dictionary<string, object>> data;
        int count;
        public double localMinutes { get; set; }
        protected override async Task OnInitializedAsync()
        {
             localMinutes = BusinessService.UserTimeSpan.Value.TotalMinutes;
            isLoading = true;
            var authstate = await AuthState.GetAuthenticationStateAsync();
            var user = authstate.User;
            reportRequest.DateFrom = DateTime.UtcNow.AddMinutes(localMinutes);
            reportRequest.DateTo = DateTime.UtcNow.AddMinutes(localMinutes);
            CustomReportRequest.DateFrom = DateTime.UtcNow;
            CustomReportRequest.DateTo = DateTime.UtcNow.AddMinutes(localMinutes);
            CustomReportRequest.TimeZoneOffset = localMinutes;
            if (user.Identity.Name != null)
            {
                businessModel = await this.localStorage.GetItemAsync<BusinessModel>("storeDetails", null);
                await LoadData();
            }
            else
            {
                this.NavigationManager.NavigateTo($"/pos/login");
            }
        }
        protected async Task LoadData()
        {
            try
            {
                isLoading = true;
                var responseListData = await this.ReportService.GetAllAsync<ResponseData<List<ReportsModel>>>();
                if (responseListData.Model.Count > 0)
                {
                    var responseData = responseListData.Model;
                    //ActiveReportId = responseData.FirstOrDefault().Id;
                    //await GenerateReportData(ActiveReportId);
                   
                    //ActiveReportId = -2;
                    //await GenerateItemSalesReport();
                    model = responseData;
                }
                ActiveReportId = -1;
                await GenerateDayEndReport();
                isLoading = false;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }
        public async void StartDateFilter(DateTime? startDate)
        {
            CustomReportRequest.DateFrom = startDate.GetValueOrDefault();
            reportRequest.DateFrom = startDate.GetValueOrDefault();
            await GenerateReportData(ActiveReportId);
        }
        public async void EndDateFilter(DateTime? endDate)
        {
            reportRequest.DateTo = endDate.GetValueOrDefault();
            CustomReportRequest.DateTo = endDate.GetValueOrDefault();
            await GenerateReportData(ActiveReportId);
        }

        protected async Task GenerateDayEndReport()
        {
            try
            {
               
                var responseListData = await this.ReportService.GenerateDayEndReportAsync(CustomReportRequest);
                if (responseListData != null)
                {
                    DayEndReportModel = responseListData.Model;
                }
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }

        protected async Task GenerateItemSalesReport()
        {
            try
            {
                var responseListData = await this.ReportService.GenerateItemSalesReportAsync(CustomReportRequest);
                if (responseListData != null)
                    ItemSalesReportModel = responseListData.Model;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }
        protected async Task GenerateComplementaryItemReport()
        {
            try
            {
                var responseListData = await this.ReportService.GenerateComplementaryItemReportAsync(CustomReportRequest);
                if (responseListData != null)
                    ItemSalesReportModel = responseListData.Model;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }
        protected async Task GenerateVoidItemReport()
        {
            try
            {
                var responseListData = await this.ReportService.GenerateVoidItemReportAsync(CustomReportRequest);
                if (responseListData != null)
                    ItemSalesReportModel = responseListData.Model;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }


        Dictionary<string, object> columns = new Dictionary<string, object>();
        protected async Task GenerateReportData(int id)
        {
            try
            {
                reportRequest.Id = id;
                isLoading = true;
                if (id == -1)
                {
                    await GenerateDayEndReport();
                }
                else if (id == -2)
                {
                    await GenerateItemSalesReport();
                }
                else if (id == -3)
                {
                    await GenerateComplementaryItemReport();
                }
                else if (id == -4)
                {
                    ActiveReportId = id;
                    await GenerateVoidItemReport();
                }
                else
                {
                   
                    var responseListData = await this.ReportService.GenerateReportDataAsync(reportRequest);
                    if (responseListData != null)
                    {
                        orderModel = responseListData.Model;
                        data = new ObjectDictionaryHelper().ReadArray(orderModel, new JsonSerializerOptions { });
                        columns = data.FirstOrDefault();
                        count = data.Count();
                    }
                }
                isLoading = false;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }
        public async Task ChangeReport(int id)
        {
            try
            {
                isLoading = true;
                if (id==-1)
                {
                    ActiveReportId = id;
                    await GenerateDayEndReport();
                }
                else if(id==-2)
                {
                    ActiveReportId = id;
                    await GenerateItemSalesReport();
                }
                else if(id==-3)
                {
                    ActiveReportId = id;
                    await GenerateComplementaryItemReport();
                }
                else if(id==-4)
                {
                    ActiveReportId = id;
                    await GenerateVoidItemReport();
                }
                else
                {
                    ActiveReportId = id;
                    await GenerateReportData(id);
                }
               
                
                isLoading = false;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }

    }
}
