//using GM.Store.Client.Infrastructure.Extensions;
//using Microsoft.AspNetCore.Components;
//using Microsoft.JSInterop;
//using System.Net;
//using Toolbelt.Blazor;

//namespace GM.Store.Client.Infrastructure
//{
//    public class HttpInterceptorService
//    {
//        private readonly HttpClientInterceptor _interceptor;
//        private readonly NavigationManager _navManager;
//        private readonly IJSRuntime _JSRuntime;
//        //public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager, IJSRuntime JSRuntime)
//        {
//            _interceptor = interceptor;
//            _navManager = navManager;
//            _JSRuntime = JSRuntime;
//        }
//        public void RegisterBeforeEvent() => _interceptor.BeforeSend += InterceptBeforeResponse;
//        public void RegisterAfterEvent() => _interceptor.AfterSend += InterceptAfterResponse;
//        private async void InterceptBeforeResponse(object sender, HttpClientInterceptorEventArgs e)
//        {
//            try
//            {
//               // await this._JSRuntime.InvokeVoidAsync("BeforeSend");
//            }
//            catch (System.Exception)
//            {

//                throw;
//            }
           
//        }
//        private async void InterceptAfterResponse(object sender, HttpClientInterceptorEventArgs e)
//        {
//            string message = string.Empty;
//            if (!e.Response.IsSuccessStatusCode)
//            {
//                var statusCode = e.Response.StatusCode;
//                switch (statusCode)
//                {
//                    case HttpStatusCode.NotFound:
//                        _navManager.NavigateTo("/404");
//                        message = "The requested resorce was not found.";
//                        break;
//                    case HttpStatusCode.Unauthorized:
//                        _navManager.NavigateTo("/unauthorized");
//                        message = "User is not authorized";
//                        break;
//                    default:
//                        _navManager.NavigateTo("/500");
//                        message = "Something went wrong, please contact Administrator";
//                        break;
//                }
//                throw new HttpResponseException(message);
//            }
//            try
//            {
//              //  await this._JSRuntime.InvokeVoidAsync("AfterSend");
//            }
//            catch (System.Exception)
//            {

//                throw;
//            }
         
//        }
//        public void DisposeEvent()
//        {
           
//            _interceptor.BeforeSend -= InterceptBeforeResponse;
//            _interceptor.AfterSend -= InterceptAfterResponse;
//        }
//    }
//}
