using GM.Store.Server.Helper;

namespace GM.Store.Server.Models
{
    //public interface ITModel
    //{
    //    public string Id { get; set; }
    //}
    public class Product
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(LongToStringJsonConverter))]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public double? SellingPrice { get; set; }
        public int? ApplicationId { get; set; }
        public string? RelativePageUrl { get; set; }
        public int? Stock { get; set; }
        public string LocalId { get; set; }
        public int LiveId { get; set; }
        public string? Alias { get; set; }
        public int CategoryId { get; set; }
        public string? ActionText { get; set; }
        public string? ActionLink { get; set; }
        public string? Tags { get; set; }
        public string? Caption { get; set; }
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


        public string ParentCategory { get; set; }
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public string Gst { get; set; }
        public string Cess { get; set; }
        public string Hsn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool? ItemToRemove { get; set; }
        public bool? IsToSync { get;  set; }
        public string? ExternalId { get; set; }
        public string? Base64Image { get; set; }
        public int BaseCategoryId { get; set; }

    }
  
}
