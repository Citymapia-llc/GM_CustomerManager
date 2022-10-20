using GM.Store.Shared.Models;
using Radzen;

namespace GM.Store.Client.Pages.POS
{
    public partial class ReceiptLog
    {
        protected BusinessModel businessModel = new BusinessModel();
        protected IList<ReceiptModel> receipts = new List<ReceiptModel>();
        protected ReceiptRequestModel model = new ReceiptRequestModel();
        protected SendSms sendSms { get; set; }
        protected bool showModal { get; set; }
        private int count { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var authstate = await AuthState.GetAuthenticationStateAsync();
            var user = authstate.User;
            if (user.Identity.Name != null)
            {
                businessModel = this.BusinessService.StoreDetails;
                model.CurrentPage = 0;
                model.PageSize = 50;
                await LoadData(new LoadDataArgs() { Skip = model.CurrentPage, Top = model.PageSize });

            }
            else
            {
                this.NavigationManager.NavigateTo($"/pos/login");
            }
        }
        protected async Task LoadData(LoadDataArgs args)
        {
            try
            {
                model.CurrentPage = args.Skip.Value;
                model.PageSize = args.Top.Value;
                var responseListData = await this.RecieptService.GetLog(model);
                receipts = responseListData.Model.List.ToList();
                var orderCount = responseListData.Model.Pager.TotalCount;
                count = orderCount;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // exception.Redirect();
            }
        }

        async Task orderDetailsById(ReceiptModel model)
        {
            await sendSms.Show(model);
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
