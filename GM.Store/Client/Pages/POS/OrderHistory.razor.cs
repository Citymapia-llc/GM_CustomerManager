using GM.Store.Shared.Models;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.Store.Client.Pages.POS
{
    public partial class OrderHistory
    {
        private int count;
        private bool isLoading;
        protected BusinessModel businessModel;
        IList<OrderHistoryResponse> selectedOrder;
        protected OrderSummary orderSummary { get; set; }
        protected OrderHistoryRequestModel model = new OrderHistoryRequestModel();
        List<OrderHistoryResponse> orders = new List<OrderHistoryResponse>();
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            var authstate = await AuthState.GetAuthenticationStateAsync();
            var user = authstate.User;
            if (user.Identity.Name != null)
            {
                businessModel = await this.localStorage.GetItemAsync<BusinessModel>("storeDetails", null);
                model.DateFrom = DateTime.UtcNow.AddMonths(-3);
                model.StartDate = model.DateFrom.ToString("dd-MMM-yyyy");
                model.DateTo = DateTime.UtcNow;
                model.EndDate = model.DateTo.ToString("dd-MMM-yyyy");
                model.skip = 0;
                model.Take = 10;
                await LoadData(new LoadDataArgs() { Skip = model.skip, Top = model.Take });
            }
            else
            {
                this.NavigationManager.NavigateTo($"/pos/login");
            }
        }
        public async void StartDateFilter(DateTime? startDate, string name, string format)
        {
            model.DateFrom = startDate.GetValueOrDefault();
            model.StartDate = model.DateFrom.ToString("dd-MMM-yyyy");
            await LoadData(new LoadDataArgs() { Skip = model.skip, Top = model.Take });
        }
        public async void EndDateFilter(DateTime? endDate, string name, string format)
        {
            model.DateTo = endDate.GetValueOrDefault();
            model.EndDate = model.DateTo.ToString("dd-MMM-yyyy");
            await LoadData(new LoadDataArgs() { Skip = model.skip, Top = model.Take });
        }
        protected async Task LoadData(LoadDataArgs args)
        {
            try
            {
                isLoading = true;
                model.skip = args.Skip.Value;
                model.Take = args.Top.Value;
                if (args.Sorts != null && args.Sorts.Count() > 0)
                {
                    string SortOrder = null;
                    string sortOrder = args.Sorts.FirstOrDefault().SortOrder.ToString();
                    switch (sortOrder.ToLower())
                    {
                        case "ascending":
                            SortOrder = "ASC";
                            break;
                        case "descending":
                            SortOrder = "DESC";
                            break;
                        default:
                            SortOrder = "DESC";
                            break;
                    }
                    model.SortKey = args.Sorts.FirstOrDefault().Property;
                    model.SortOrder = SortOrder;
                }

                var responseListData = await this.OrderHistoryService.GetAllOrders(model);
                if (responseListData.Model.Count > 0)
                {
                    orders = responseListData.Model;
                    var orderCount = await this.OrderHistoryService.GetOrderHistoryCount(model);
                    count = orderCount.Model;
                }
                isLoading = false;
                StateHasChanged();
            }
            catch (Exception exception)
            {
                // exception.Redirect();
            }
        }
        async Task orderDetails(string orderId)
        {
            await orderSummary.ShowOrderDetal(orderId);
        }
        async Task orderDetailsById(int orderId)
        {
            await orderSummary.ShowOrderDetailById(orderId);
        }
        async Task RowSelect(OrderHistoryResponse customer)
        {
            await orderSummary.ShowOrderDetal(customer.OrderId);
        }
        private void OrderDetailsClose()
        {
            selectedOrder = null;
        }
    }
}
