using GM.Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GM.Store.Client.Infrastructure.Services.POS.Product
{
    public class ProductService:IProductService
    {
        private readonly HttpClient http;

        private const string ProductPath = "api/products";

        private readonly IDataManager _dataManager;
        public ProductService(HttpClient http, IDataManager dataManager)
        {
            this.http = http;
            _dataManager = dataManager;
        }

        #region old codes
        public async Task<ResponseListData<ProductModel>> GetAll(ProductRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseListData<ProductModel>>(model, ProductPath);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseData<List<CustomerResponseModel>>> GetAllCustomer(CustomerFilterModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<List<CustomerResponseModel>>>(model, "api/business/customers");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseData<CustomerResponseModel>> AddCustomer(CustomerRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<CustomerResponseModel>>(model, "api/business/addcustomer");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion


        public async Task<ResponseListData<ProductModel>> GetAllProducts(ProductRequestModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseListData<ProductModel>>(model, "api/product/all");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ProductImportResponse> ImportProducts(UploadedFile model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ProductImportResponse>(model, "api/product/import");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateProduct(ProductModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<bool>(model, "api/product/UpdateProduct");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteProduct(ProductModel model)
        {
            try
            {
                var response = await _dataManager.PostAsync<bool>(model, "api/product/deleteproduct");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseData<List<SyncProductResponsetModel>>> SyncProducts(SyncProductRequest model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ResponseData<List<SyncProductResponsetModel>>>(model, "api/product/Sync");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UploadImage(UploadedFile model)
        {
            try
            {
                var response = await _dataManager.PostAsync<ProductImportResponse>(model, "api/product/uploadimage");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
