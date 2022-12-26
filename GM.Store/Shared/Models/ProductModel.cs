using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
    public class ProductRequestModel
    {
        public string? Keyword { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int Sort { get; set; }
        public int[]? CategoryIds { get; set; }
        public int BaseCategoryId { get; set; }
    }
    public class RelatedProductRequestModel
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int CategoryId { get; set; }
    }
    public class ResponseData
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public bool Succeeded => Status == 200;
    }

    public class ResponseListData<T> : ResponseData
    {
        public ResponseListData()
        {
            Model = new GMList<T>();
        }
        public GMList<T> Model { get; set; }

    }
    public class ResponseData<T> : ResponseData
    {
        public T Model { get; set; }

    }
    public class ResponseSmsData<T> : ResponseData
    {
        public T data { get; set; }

    }

    public class GMList<T>
    {
        public IEnumerable<T> List { get; set; }
        public GMPager Pager { get; set; }
    }
    public interface IGMPager
    {
        int PageSize { get; set; }
        int CurrentPage { get; set; }
        int TotalCount { get; set; }
        int PendingSyncCount { get; set; }
    }
    public class GMPager : IGMPager
    {
        public GMPager()
        {
            PageSize = 10;
            CurrentPage = 1;
        }
        private int pageSize;
        private int pageIndex;

        public int PageSize { get => pageSize; set => pageSize = value; }
        /// <summary>
        /// Page index starts from 1
        /// </summary>
        public int CurrentPage { get => pageIndex; set => pageIndex = value; }
        public int TotalCount { get; set; }
        public int PendingSyncCount { get; set; }

    }
    public class ProductModel
    {
        public string Id { get; set; }
        public string LocalId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public double? SellingPrice { get; set; }
        public int? ApplicationId { get; set; }
        public string? RelativePageUrl { get; set; }
        public int? Stock { get; set; }
        public string? Alias { get; set; }
        public int CategoryId { get; set; }
        public string? ActionText { get; set; }
        public string? ActionLink { get; set; }
        public string? Caption { get; set; }
        public string? Tags { get; set; }
        public int? CallToAction { get; set; }
        public bool? COD { get; set; }
        public DateTime? UpdatedDtm { get; set; }
        public bool? RequestQuote { get; set; }
        public string? WebUrl { get; set; }
        public int OfferPercentage { get; set; }
        public string? ImageUrl { get; set; }
        public double? ProductAmount { get; set; }
        public double? SellingAmount { get; set; }
        public List<int>? CategoryIds { get; set; }


        public string? ParentCategory { get; set; }
        public string? Category { get; set; }
        public string? ProductCode { get; set; }
        public string? Gst { get; set; }
        public string? Cess { get; set; }
        public string? Hsn { get; set; }
        public bool? ItemToRemove { get; set; }
        public bool? IsToSync { get; set; }
        public int BaseCategoryId { get; set; }
    }
    public class ProductImportResponse
    {
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public bool IsProcessComplete { get; set; }
    }
    public class SyncProductResponsetModel
    {
        public string? LocalId { get; set; }
        public int LiveId { get; set; }
        public bool IsSyncSuccess { get; set; }
        public string? ImageUrl { get; set; }
    }
    public class SyncProductRequest
    {
        public string[]? LocalIds { get; set; }
        public int BaseCategoryId { get; set; }
    }
    public class ImageUploadResponsetModel
    {
        public string? Base64Image { get; set; }
        public string? LocalId { get; set; }
        public bool? IsUploaded { get; set; }
        public string? ImageUrl { get; set; }
    }
}
