namespace GM.Store.Client.Infrastructure.Services
{
    using GM.Store.Shared.Models;
    using System.Threading.Tasks;
    public interface IAuthService
    {
        Task<ResponseData<LoginResponseModel>> Login(LoginModel model);
        Task<ResponseData<AdminLoginResponseModel>> AdminLogin(LoginModel model,string url);
        Task logout();
        Task<ResponseData<string>> OTPVerifyAsync(OtpVerificationModel model);
        Task<ResponseData<LoginResponseModel>> RegisterAsync(RegisterModel model);
        Task<ResponseData<OtpVerificationModel>> ResendOtp(ResendOtpRequestModel model);

    }
}
