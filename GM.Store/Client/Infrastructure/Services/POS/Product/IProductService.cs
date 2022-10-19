using GM.Store.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GM.Store.Client.Infrastructure.Services.POS.Product
{
    public interface IProductService
    {
        Task<ResponseListData<ProductModel>> GetAll(ProductRequestModel model);
        Task<ResponseData<List<CustomerResponseModel>>> GetAllCustomer(CustomerFilterModel model);
        Task<ResponseData<CustomerResponseModel>> AddCustomer(CustomerRequestModel model);
        Task<ResponseListData<ProductModel>> GetAllProducts(ProductRequestModel model);
        Task<ProductImportResponse> ImportProducts(UploadedFile model);
        Task<bool> UpdateProduct(ProductModel model);
        Task<bool> DeleteProduct(ProductModel model);
        Task<ResponseData<List<SyncProductResponsetModel>>> SyncProducts(SyncProductRequest model);
        Task<bool> UploadImage(UploadedFile model);
    }
}
