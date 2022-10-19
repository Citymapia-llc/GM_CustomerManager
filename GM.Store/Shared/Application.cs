using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Shared
{
    public class Application
    
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
        public string AuthToken { get; set; }
        public string BusinessCurrencySymbol { get; set; }
        public int CountryId { get; set; }
    }
}
