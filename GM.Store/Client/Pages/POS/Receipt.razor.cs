using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace GM.Store.Client.Pages.POS
{
    public partial class Receipt
    {
        protected BusinessModel businessModel = new BusinessModel();
        protected IList<ReceiptModel> receipts = new List<ReceiptModel>();
        protected ReceiptRequestModel model = new ReceiptRequestModel();
        protected SmsBalanceModel smsModel = new SmsBalanceModel();
        protected SendSms sendSms { get; set; }
        private bool isSmsBalanceLoading;
        private bool isLoading;
        private int count { get; set; }
        private string ErrorMessage { get; set; }
        private bool showModal { get; set; }
        protected ProductImportResponse productImportResponse = new ProductImportResponse();

        Radzen.Blazor.RadzenDataGrid<ReceiptModel> productGrid;
        int uploadLoader = 0;
        IReadOnlyList<IBrowserFile> selectedFiles;
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            ErrorMessage = null;
           
            var fileExt = Path.GetExtension(e.File.Name).Substring(1);
            if (!(fileExt == "xls" || fileExt == "xlsx"))
            {
                ErrorMessage = "File is not supported!";

            }
            else
            {
                selectedFiles = e.GetMultipleFiles();
            }
            this.StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await this.localStorage.SetItemAsync("CurrentPortal", "pos");
            this.BusinessService.CurrentPortal = "pos";
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
                var responseListData = await this.RecieptService.GetAll(model);
                if (responseListData != null && responseListData.Succeeded)
                {
                    receipts = responseListData.Model.List.ToList();
                    var orderCount = responseListData.Model.Pager.TotalCount;
                    count = orderCount;
                }
                isLoading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // exception.Redirect();
            }
        }
        public async void SearchFilter(string keyword)
        {
            model.Keyword = keyword;
            StateHasChanged();
            await LoadData(new LoadDataArgs() { Skip = model.CurrentPage, Top = model.PageSize });
        }
        async Task orderDetailsById(ReceiptModel model)
        {
            await sendSms.Show(model);
        }

        private async void OnSubmit()
        {
            try
            {
                uploadLoader = 2;
                this.StateHasChanged();
                foreach (var file in selectedFiles)
                {
                    long maxFileSize = 1024 * 100000;
                    Stream stream = file.OpenReadStream(maxFileSize);
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    UploadedFile uploadedFile = new UploadedFile();
                    uploadedFile.ApplicationId = businessModel.Id;
                    uploadedFile.FileName = file.Name;
                    uploadedFile.FileContent = ms.ToArray();
                    ms.Close();
                    var ImportSuccess = await this.RecieptService.Import(uploadedFile);
                    if (ImportSuccess != null && ImportSuccess.IsProcessComplete)
                    {
                        productImportResponse = ImportSuccess;
                    }
                    else
                    {
                        uploadLoader = 1;
                    }
                    this.StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            this.StateHasChanged();
        }

        public void ModalOpen()
        {
            showModal = true;
            uploadLoader = 0;
            ErrorMessage = null;
            productImportResponse = new ProductImportResponse();
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
        public async Task CheckBalance()
        {
            isSmsBalanceLoading=true;
            var balanceObj = await this.RecieptService.CheckSmsBalanace();
            if (balanceObj != null && balanceObj.Succeeded)
            {
                smsModel = balanceObj.data;
            }
            isSmsBalanceLoading=false;
            StateHasChanged();
        }

    }

}
