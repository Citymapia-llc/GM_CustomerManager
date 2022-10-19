using System;
using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
    public class BusinessModel
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string DomainUrl { get; set; }
        public string ApplicationToken { get; set; }
        public bool? IsActive { get; set; }
        public bool? ShowInListing { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ApplicationRelativeUrl { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public bool? IsPhoneConfirmed { get; set; }
        public bool? BusinessEmailConfirmed { get; set; }
        public string BusinessTitle { get; set; }
        public string WhatsappNumber { get; set; }
        public string LandPhone { get; set; }
        public bool? ShowInEmail { get; set; }
        public bool? ShowInPhoneNumber { get; set; }
        public string Logo { get; set; }
        public string Favicon { get; set; }
        private string Banner { get; set; }
        public List<string> AssetUrl { get; set; }
        public string BusinessPhoneFormat { get; set; }
        public string BusinessPhoneCode { get; set; }
        public string BusinessCurrencyCode { get; set; }
        private string _CDNHost { get; set; }
        public string CDNHost { get; set; }
        public bool? IsGroupBusiness { get; set; }
        public bool? IsDefault { get; set; }
        public string LanguageKey { get; set; }
        public bool? Payment { get; set; }
        public bool COD { get; set; }
        public bool EnableStore { get; set; }
        public string AuthToken { get; set; }
        public string BusinessCurrencySymbol { get; set; }
        public int CountryId { get; set; }
        public int? CitymapiaVersion { get; set; }
        public List<PaymentGateWays> PaymentGateWays { get; set; }
        public List<BusinessAddressModel> Address { get; set; }
        public PageModel CmsPage { get; set; }
    }
    public class PaymentGateWays
    {
        public int Id { get; set; }
        public int GatewayId { get; set; }
        public string Title { get; set; }
    }
    public class BusinessThemeModel
    {
        public string CustomCss { get; set; }
    }
    public class BusinessAddressModel
    {
        public int Id { get; set; }
        public int? EntityType { get; set; }
        public int? ParentId { get; set; }
        public string Location { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Area { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ShowInListing { get; set; }
        public string AdditionalInfo { get; set; }
        public int Radius { get; set; }
        public string Title { get; set; }
    }
}
