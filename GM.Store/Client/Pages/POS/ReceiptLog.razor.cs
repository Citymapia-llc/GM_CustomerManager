using GM.Store.Shared.Models;
using Radzen;

namespace GM.Store.Client.Pages.POS
{
    public partial class ReceiptLog
    {
        protected BusinessModel businessModel = new BusinessModel();
        protected IList<ConfirmedReciept> receipts = new List<ConfirmedReciept>();
        protected ReceiptRequestModel model = new ReceiptRequestModel();
        protected ConfirmedReciept modalDetails { get; set; }
        protected bool showModal { get; set; }
        private int count { get; set; }
        public double localMinutes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            localMinutes = BusinessService.UserTimeSpan.Value.TotalMinutes;
            var authstate = await AuthState.GetAuthenticationStateAsync();
            var user = authstate.User;
            if (user.Identity.Name != null)
            {
                businessModel = this.BusinessService.StoreDetails;
                model.CurrentPage = 0;
                model.PageSize = 10;
                await LoadData(new LoadDataArgs() { Skip = model.CurrentPage, Top = model.PageSize });

            }
            else
            {
                this.NavigationManager.NavigateTo($"/pos/login");
            }
        }
        public async void SearchFilter(string keyword)
        {
            model.Keyword = keyword;
            StateHasChanged();
            await LoadData(new LoadDataArgs() { Skip = model.CurrentPage, Top = model.PageSize });
        }
        protected async Task LoadData(LoadDataArgs args)
        {
            try
            {
                model.CurrentPage = args.Skip.Value;
                model.PageSize = args.Top.Value;
                var responseListData = await this.RecieptService.GetLog(model);
                if (responseListData != null && responseListData.Succeeded)
                {
                    receipts = responseListData.Model.List.ToList();
                    var orderCount = responseListData.Model.Pager.TotalCount;
                    count = orderCount;
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // exception.Redirect();
            }
        }

        async Task orderDetailsById(ConfirmedReciept model)
        {
            modalDetails = model;
            showModal = true;
            StateHasChanged();
        }

        

        public void ModalOpen()
        {
            showModal = true;
            StateHasChanged();
        }
        public async Task CloseModal()
        {
            showModal = false;
            StateHasChanged();
        }
    }
}
