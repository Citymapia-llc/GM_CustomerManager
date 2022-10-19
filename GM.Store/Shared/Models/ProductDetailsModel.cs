using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
	
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public double? Amount { get; set; }
        public double? SellingPrice { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string MetaKeyword { get; set; }
        public string Metadescription { get; set; }
        public int CategoryId { get; set; }
        public int? Stock { get; set; }
        public int OfferPercentage { get; set; }
        public string RelativePageUrl { get; set; }
        public IEnumerable<string> ImageUrl { get; set; }
        public bool? COD { get; set; }
        public double? DeliveryCharges { get; set; }
        public int? ExpectedDay { get; set; }
        public string PageTitle { get; set; }
        public string UPC { get; set; }
        public string ActionLink { get; set; }
        public string ActionText { get; set; }
        public int? CallToAction { get; set; }
        public string CanconialUrl { get; set; }
        public string Caption { get; set; }
        public bool? RequestQuote { get; set; }
        public int? ApplicationId { get; set; }
        public bool IsWishlist { get; set; }
        public int ReviewCount { get; set; }
        public bool? EnableSelling { get; set; }

        public string BaseCategory { get; set; }
        public int BaseCategoryId { get; set; }
        public string BaseCategoryPageUrl { get; set; }
        public double? ProductAmount { get; set; }
        public double? SellingAmount { get; set; }
    }
}
