using System.Threading.Tasks;
using GM.Store.Shared.Models;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace GM.Store.Client.Pages.UserAccount
{
    public partial class Login
    {
        [Inject]
        public NavigationManager MyNavigationManager { get; set; }
        private readonly LoginModel model = new LoginModel();
        private readonly OtpVerificationModel otpModel = new OtpVerificationModel();
        private bool isOtp;
        private bool isLoading = false;
        private bool isResendLoading = false;
        private string warningMessage { get; set; }
        private string relativeUrl { get; set; }
        private string registerUrl { get; set; }
        private BusinessModel businessModel;
        protected override void OnInitialized()
        {
            businessModel = BusinessService.StoreDetails; //await this.localStorage.GetItemAsync<BusinessModel>("storeDetails", null);
            relativeUrl = MyNavigationManager.ToBaseRelativePath(MyNavigationManager.Uri).ToLower();
            registerUrl = $"/account/register";
            if (relativeUrl.Contains("returnurl="))
            {
                registerUrl = $"/account/register?{relativeUrl.Split('?')[1]}";
            }
        }
        private void CheckEmailORPhone()
        {
            Regex regex = new Regex(@"^[a-zA-Z]{1}");
            Regex regexphone = new Regex(@"^[0-9]{1}");
            if (regex.Match(model.EmailPhone).Success)
            {
                model.UserName = model.EmailPhone;
                warningMessage = "";
            }
            else if (regexphone.Match(model.EmailPhone).Success)
            {
                model.PhoneNumber = model.EmailPhone;
                warningMessage = "";
            }
            else
            {
                model.PhoneNumber = null;
                model.UserName = null;
                warningMessage = "Invalid EmailID or Mobile Number";
            }

            StateHasChanged();
            return;
        }

        private async Task SubmitAsync()
        {

            if (model.UserName != null)
            {
                Regex regexEmail = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
                Match match = regexEmail.Match(model.EmailPhone);
                if (match.Success)
                {
                    model.LoginType = "Email";
                    warningMessage = "";
                }
                else
                {
                    warningMessage = "not a valid EmailID";
                    return;
                }
            }

            if (model.PhoneNumber != null)
            {
                if (Regex.IsMatch(model.EmailPhone, @"(\d*-)?\d{10}", RegexOptions.IgnoreCase))
                {
                    model.LoginType = "Phone_Number";
                    warningMessage = "";
                }
                else
                {
                    warningMessage = "Not a valid mobile Number";
                    return;
                }
            }
            isLoading = true;
            var result = await this.AuthService.Login(this.model);
            if (result != null && result.Succeeded)
            {
                isOtp = true;
                otpModel.UserId = result.Model.UserId;
                otpModel.LoginType = result.Model.LoginType;
            }
            else
            {
                warningMessage = result.Message;
            }
            isLoading = false;
            this.StateHasChanged();
        }

        private async Task OTPSubmitAsync()
        {
            isLoading = true;

            var result = await this.AuthService.OTPVerifyAsync(this.otpModel);
            if (result != null && result.Succeeded)
            {
                var returnUrl = "";
                if (relativeUrl.Contains("returnurl="))
                {
                   // returnUrl = HttpUtility.UrlEncode(relativeUrl.Split("returnurl=")[1]);
                    returnUrl = relativeUrl.Split("returnurl=")[1];
                }
                this.NavigationManager.NavigateTo($"/{returnUrl}");
            }
            else
            {
                warningMessage = result.Message;
            }
            isLoading = false;
        }
        private async void ResendOtp()
        {
            isResendLoading = true;

            var result = await this.AuthService.ResendOtp(new ResendOtpRequestModel { LoginType = otpModel.LoginType, UserId = otpModel.UserId });
            if (result != null && result.Succeeded)
            {
                isOtp = true;
                otpModel.UserId = result.Model.UserId;
                otpModel.LoginType = result.Model.LoginType;
            }
            else
            {
                warningMessage = result.Message;
            }
            isResendLoading = false;
            this.StateHasChanged();
        }
    }
}