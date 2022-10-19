namespace GM.Store.Client.Infrastructure.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using Infrastructure;
    using System;
    using GM.Store.Shared.Models;
    //public class DataManager
    //{
    //    //
    //    public static bool PostAsync<T>(T model,string apiEndPoint)
    //    {
    //        //Check If Local storage is enabled
    //        //try to fetch data from local db - IndexDb if Application is PWA, Sqlite if application is Client Server
    //        //If local Data not avialable fetch from API
    //    }
    //}
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly HttpClient httpClient;
        private readonly IDataManager _dtManager;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider,IDataManager dataManager)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
            _dtManager = dataManager;
        }

        public async Task<ResponseData<LoginResponseModel>> Login(LoginModel model)
        {
            var response = await this.httpClient.PostAsJsonAsync("Account/Tokens", model);
            var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<LoginResponseModel>>();
            if (responseObject != null)
            {
                return responseObject;
            }
            return null;
        }
        public async Task<ResponseData<AdminLoginResponseModel>> AdminLogin(LoginModel model, string url)
        {
            var responseObject = await this._dtManager.PostAsync<ResponseData<AdminLoginResponseModel>>(model,"api/account/adminlogin");
           
            if (responseObject != null)
            {
                try
                {
                    if (responseObject.Model != null)
                    {
                        if(url=="supplier")
                        {
                            if(!responseObject.Model.UserRoles.Contains("Supplier"))
                            {
                                return new ResponseData<AdminLoginResponseModel> { Status= 400,Message= "Insufficient role to Login!" };
                            }
                        }
                        else
                        {
                            if (responseObject.Model.UserRoles.Contains("Supplier"))
                            {
                                return new ResponseData<AdminLoginResponseModel> { Status = 400, Message = "You have no permission to login!" };
                            }
                        }
                        var token = responseObject.Model;
                        await this.localStorage.SetItemAsync($"authToken-{url}", token.AuthToken);
                        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AuthToken);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                return responseObject;
            }
            return null;
        }
        public async Task<ResponseData<LoginResponseModel>> RegisterAsync(RegisterModel model)
        {
            var response = await this.httpClient.PostAsJsonAsync("business/Register", model);
            var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<LoginResponseModel>>();
            if (responseObject != null)
            {
                return responseObject;
            }
            return null;
        }
        public async Task<ResponseData<string>> OTPVerifyAsync(OtpVerificationModel model)
        {
            var response = await this.httpClient.PostAsJsonAsync("Account/OtpVerification", model);
            var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<string>>();
            if (responseObject != null)
            {
                try
                {
                    if (responseObject.Model != null)
                    {
                        var token = responseObject.Model;
                        await this.localStorage.SetItemAsync("authToken", token);
                        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                return responseObject;
            }
            return null;
        }

        public async Task<ResponseData<OtpVerificationModel>> ResendOtp(ResendOtpRequestModel model)
        {
            var response = await this.httpClient.PostAsJsonAsync("Account/ResendOtp", model);
            var responseObject = await response.Content.ReadFromJsonAsync<ResponseData<OtpVerificationModel>>();
            if (responseObject != null)
            {
                return responseObject;
            }
            return null;
        }
        public async Task logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");

            ((ApiAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsLoggedOut();

            this.httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
