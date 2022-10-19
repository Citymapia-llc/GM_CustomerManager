using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Web;

namespace GM.Store.Client.Pages.UserAccount
{
    public partial class Register
    {
        [Inject]
        public NavigationManager MyNavigationManager { get; set; }
        private bool IsSignUpWithPhone = true;
        private bool isLoading = false;
        private bool isResendLoading = false;
        private bool isOtp;
        private RegisterModel model = new RegisterModel();
        public string LoginType = "Phone_Number";
        public string warningMessage;
        private readonly OtpVerificationModel otpModel = new OtpVerificationModel();

        private BusinessModel businessModel;
        protected override async Task OnInitializedAsync()
        {
            businessModel = BusinessService.StoreDetails;//await this.localStorage.GetItemAsync<BusinessModel>("storeDetails", null);
        }

        public void SwitchSignUp(int type)
        {
            isOtp = false;
            if (type == 2)
            {
                IsSignUpWithPhone = false;
                LoginType = "Email";
            }
            else
            {
                IsSignUpWithPhone = true;
                LoginType = "Phone_Number";
            }
            warningMessage = null;
            model = new RegisterModel();
            StateHasChanged();
        }

        public async Task SignUpSubmitAsync()
        {
            if (!model.TermsandConditions)
            {
                warningMessage = "Terms and condition field is required !";
                return;
            }
            isLoading = true;
            model.LoginType = LoginType;
            otpModel.LoginType = LoginType;
            var result = await this.AuthService.RegisterAsync(this.model);
            if (result != null && result.Succeeded)
            {
                isOtp = true;
                otpModel.UserId = result.Model.UserId;
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
                var relativeUrl = MyNavigationManager.ToBaseRelativePath(MyNavigationManager.Uri).ToLower();
                var returnUrl = "";
                if (relativeUrl.Contains("returnurl="))
                {
                    returnUrl = HttpUtility.UrlEncode(relativeUrl.Split("returnurl=")[1]);
                }
                this.NavigationManager.NavigateTo($"{returnUrl}");
            }
            else
            {
                //  warningMessage = result.Message;
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
