using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GM.Store.Shared.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailPhone { get; set; }
        public string? LoginType { get; set; }
    }

    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public string Otp { get; set; }
        public string LoginType { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceToken { get; set; }
    }
    public class OtpVerificationModel
    {
        public int UserId { get; set; }
        public string Otp { get; set; }
        public string LoginType { get; set; }

    }
    public class ResendOtpRequestModel
    {
        public int UserId { get; set; }
        public string LoginType { get; set; }

    }
    public class AdminLoginResponseModel
    {
        public string AuthToken { get; set; }
        public List<string> UserRoles { get; set; }
    }
    public class PosNavigationModel
    {
        public int TabId { get; set; }
        public string Url { get; set; }
    }
}
