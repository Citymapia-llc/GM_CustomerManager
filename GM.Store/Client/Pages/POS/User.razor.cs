using GM.Store.Shared.Models;
using Radzen;

namespace GM.Store.Client.Pages.POS
{
    public partial class User
    {
        protected BusinessModel businessModel = new BusinessModel();

        protected IList<UserResponseModel> users = new List<UserResponseModel>();

        protected UserRequestModel model = new UserRequestModel();

        private bool isLoading;
        private int count { get; set; }
        private bool showModal { get; set; }

        protected ProductImportResponse productImportResponse = new ProductImportResponse();

        Radzen.Blazor.RadzenDataGrid<ReceiptModel> productGrid;
        int uploadLoader = 0;
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
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
                isLoading = true;
                isLoading = true;
                productImportResponse = new ProductImportResponse();
                model.CurrentPage = args.Skip.Value;
                model.PageSize = args.Top.Value;
                var responseListData = await this.UserService.GetAllAsync(model);
                if (responseListData != null && responseListData.Succeeded)
                {
                    users = responseListData.Model.List.ToList();
                    var userCount = responseListData.Model.Pager.TotalCount;
                    count = userCount;
                }
                isLoading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // exception.Redirect();
            }
        }

        async Task orderDetailsById(UserResponseModel model)
        {
           // await sendSms.Show(model);
        }

        

        public void ModalOpen()
        {
            showModal = true;
            StateHasChanged();
        }
        public async Task CloseModal()
        {
            showModal = false;
            await productGrid.Reload();
            StateHasChanged();
        }
        public async Task SendSmsClose()
        {
            await productGrid.Reload();
            StateHasChanged();
        }
    }
}
