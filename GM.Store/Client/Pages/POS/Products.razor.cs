using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
namespace GM.Store.Client.Pages.POS
{
    public partial class Products
    {
        IList<ProductModel> selectedProducts = new List<ProductModel>();
        protected BusinessModel businessModel = new BusinessModel();
        protected List<BaseCategory> BaseCategory = new List<BaseCategory>();
        private int SelectedBaseCategoryId { get; set; }
        private int count { get; set; }
        private int PendingSyncCount { get; set; }
        private bool isLoading { get; set; }
        private bool showModal { get; set; }
        private bool showSyncModal { get; set; }
        private bool isSyncLoading { get; set; } = false;
        private bool isSyncComplete { get; set; } = false;
        private int SyncSuccessCount { get; set; }
        private int SyncFailedCount { get; set; }
        private string SyncErrorMessage { get; set; }

        protected ProductRequestModel model = new ProductRequestModel();
        protected ProductImportResponse productImportResponse = new ProductImportResponse();
        List<ProductModel> products = new List<ProductModel>();
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            isSyncComplete = false;
            await this.localStorage.SetItemAsync("CurrentPortal", "pos");
            this.BusinessService.CurrentPortal = "pos";
            var authstate = await AuthState.GetAuthenticationStateAsync();
            var user = authstate.User;
            if (user.Identity.Name != null)
            {
                businessModel = this.BusinessService.StoreDetails;
                if (this.BusinessService.IsListEnabled)
                {
                    var baseCategory = await this.BusinessService.GetListAsync<ResponseData<List<BaseCategory>>>();
                    if (baseCategory.Succeeded)
                    {
                        BaseCategory = baseCategory.Model;
                        if (BaseCategory != null && BaseCategory.Count() > 0)
                            SelectedBaseCategoryId = BaseCategory.Select(a => a.Id).FirstOrDefault();
                    }
                }
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
                isSyncComplete = false;
                SyncSuccessCount = 0;
                SyncFailedCount = 0;
                SyncErrorMessage = string.Empty;
                productImportResponse = new ProductImportResponse();
                model.CurrentPage = args.Skip.Value;
                model.PageSize = args.Top.Value;
                model.BaseCategoryId = SelectedBaseCategoryId;
                #region Sort table
                //if (args.Sorts != null && args.Sorts.Count() > 0)
                //{
                //    string SortOrder = null;
                //    string sortOrder = args.Sorts.FirstOrDefault().SortOrder.ToString();
                //    switch (sortOrder.ToLower())
                //    {
                //        case "ascending":
                //            SortOrder = "ASC";
                //            break;
                //        case "descending":
                //            SortOrder = "DESC";
                //            break;
                //        default:
                //            SortOrder = "DESC";
                //            break;
                //    }
                //    model.SortKey = args.Sorts.FirstOrDefault().Property;
                //    model.SortOrder = SortOrder;
                //} 
                #endregion
                var responseListData = await this.ProductService.GetAllProducts(model);
                products = responseListData.Model.List.ToList();
                selectedProducts = products.Where(a => a.IsToSync == true).ToList();
                var orderCount = responseListData.Model.Pager.TotalCount;
                count = orderCount;
                var pendingSyncCount = responseListData.Model.Pager.PendingSyncCount;
                PendingSyncCount = pendingSyncCount;
                isLoading = false;
                StateHasChanged();
            }

            catch (Exception exception)
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
        //public async Task SyncProdct()
        //{
        //    isSyncLoading = true;
        //    if (selectedProducts != null && selectedProducts.Count() > 0)
        //    {
        //        var selectedLocalIds = selectedProducts.Select(a => a.LocalId).ToList();
        //        var syncSuccess = await this.ProductService.SyncProducts(selectedLocalIds.ToArray());
        //        await productGrid.Reload();
        //    }
        //    isSyncLoading = false;
        //}


        int uploadLoader = 0;
        IReadOnlyList<IBrowserFile> selectedFiles;
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            this.StateHasChanged();
        }
        private async void OnSubmit()
        {
            try
            {
                uploadLoader = 2;
                this.StateHasChanged();
                foreach (var file in selectedFiles)
                {
                    long maxFileSize = 1024 * 10000;
                    Stream stream = file.OpenReadStream(maxFileSize);
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    UploadedFile uploadedFile = new UploadedFile();
                    uploadedFile.ApplicationId = businessModel.Id;
                    uploadedFile.FileName = file.Name;
                    uploadedFile.FileContent = ms.ToArray();
                    uploadedFile.BaseCategoryId =SelectedBaseCategoryId;
                    ms.Close();
                    var ImportSuccess = await this.ProductService.ImportProducts(uploadedFile);
                    if (ImportSuccess != null && ImportSuccess.IsProcessComplete)
                    {
                        productImportResponse=ImportSuccess;
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
        public async Task OpenSyncModal()
        {
            showSyncModal = true;
            StateHasChanged();
        }
        public async Task CloseSyncModal()
        {
            showSyncModal = false;
            await productGrid.Reload();
            StateHasChanged();
        }
        private async void SyncNow()
        {
            isSyncLoading = true;
            if (selectedProducts != null && selectedProducts.Count() > 0)
            {
                isSyncComplete = true;
                var selectedLocalIds = selectedProducts.Select(a => a.LocalId).ToList();
                var syncSuccess = await this.ProductService.SyncProducts(new SyncProductRequest() { LocalIds = selectedLocalIds.ToArray(), BaseCategoryId = SelectedBaseCategoryId });
                if (syncSuccess != null && syncSuccess.Succeeded)
                {
                    if(syncSuccess.Model!=null)
                    {
                        SyncSuccessCount= syncSuccess.Model.Where(a=>a.IsSyncSuccess).Count();
                        SyncFailedCount = syncSuccess.Model.Where(a=>!a.IsSyncSuccess).Count();
                    }
                    else
                    {
                        SyncErrorMessage = "Sync Failed.";
                    }
                }
            }
            isSyncLoading = false;
            StateHasChanged();
        }

        private async void OnImageProgress(UploadProgressArgs args, string name)
        {
            Console.WriteLine($"{args.Progress}% '{name}' / {args.Loaded} of {args.Total} bytes.");

            if (args.Progress == 100)
            {
                foreach (var file in args.Files)
                {
                    Console.WriteLine($"Uploaded: {file.Name} / {file.Size} bytes");
                    await productGrid.Reload();
                }
            }
        }
        IReadOnlyList<IBrowserFile> selectedImages;
        private async void OnImageUploadChange(InputFileChangeEventArgs e, string localId)
        {
            selectedImages = e.GetMultipleFiles();
            this.StateHasChanged();
            foreach (var file in selectedImages)
            {
                long maxFileSize = 1024 * 10000;
                Stream stream = file.OpenReadStream(maxFileSize);
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                UploadedFile uploadedFile = new UploadedFile();
                uploadedFile.ApplicationId = businessModel.Id;
                uploadedFile.FileName = file.Name;
                uploadedFile.LocalId = localId;
                uploadedFile.FileContent = ms.ToArray();
                ms.Close();
                var ImportSuccess = await this.ProductService.UploadImage(uploadedFile);
                this.StateHasChanged();
            }
        }

        #region edit delete
        Radzen.Blazor.RadzenDataGrid<ProductModel> productGrid;
        async Task EditRow(ProductModel product)
        {
            await productGrid.EditRow(product);
        }

        ProductModel orderToInsert;
        async Task SaveRow(ProductModel product)
        {
            if (product == orderToInsert)
            {
                orderToInsert = null;
            }
            var updateSuccess = await this.ProductService.UpdateProduct(product);
            if (updateSuccess)
            {
                selectedProducts.Add(product);
            }
            await productGrid.UpdateRow(product);
        }
        async Task CancelEdit(ProductModel product)
        {
            if (product == orderToInsert)
            {
                orderToInsert = null;
            }

            productGrid.CancelEditRow(product);
            await productGrid.Reload();
        }

        async Task DeleteRow(ProductModel product)
        {
            if (product == orderToInsert)
            {
                orderToInsert = null;
            }

            if (products.Contains(product))
            {
                var updateSuccess = await this.ProductService.DeleteProduct(product);
                // For demo purposes only
                products.Remove(product);
                await productGrid.Reload();
            }
            else
            {
                productGrid.CancelEditRow(product);
            }
        }
        #endregion
        public async Task SelectBaseCategory(object value)
        {
            int baseCategoryId = value is IEnumerable<int> ? 0 : Convert.ToInt32(value);
            SelectedBaseCategoryId = baseCategoryId;
            await LoadData(new LoadDataArgs() { Skip = model.CurrentPage, Top = model.PageSize });
        }
        void OnChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            Console.WriteLine($"{name} value changed to {str}");
        }
    }
}
#region  old code check box

//private bool showSync { get; set; } = false;
//void SelectAllChange(bool value)
//{
//    selectedProducts = value ? products.ToList() : null;
//    checkVisibility();
//}

//void checkboxChange(bool value, ProductModel product)
//{
//    if (!allowRowSelectOnRowClick)
//    {
//        productGrid.SelectRow(product);
//    }
//    if(value)
//    {
//        if(selectedProducts == null)
//        {
//            selectedProducts = new List<ProductModel>() { product };
//            //selectedProducts.Add(product);
//        }else
//        {
//            selectedProducts.Add(product);
//        }
//        //selectedProducts = products.Where(a=>a.LocalId==product.LocalId).ToList();
//    }
//    checkVisibility();
//}
//private bool checkVisibility()
//{
//    if (selectedProducts != null && selectedProducts.Count() > 0)
//        showSync = true;
//    else
//        showSync = false;

//    this.StateHasChanged();
//    return true;
//} 
#endregion