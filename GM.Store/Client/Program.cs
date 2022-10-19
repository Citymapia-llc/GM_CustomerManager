using Append.Blazor.Printing;
using Blazored.LocalStorage;
using Blazored.Toast;
using GM.Store.Client;
using GM.Store.Client.Data;
using GM.Store.Client.Infrastructure;
using GM.Store.Client.Infrastructure.Services;
using GM.Store.Client.Infrastructure.Services.POS.Product;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
//using SqliteWasmHelper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredToast();
builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight; });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

builder.Services.AddScoped<IDataManager, DataManager>();

builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOrderHistoryService, OrderHistoryService>();
builder.Services.AddScoped<IPrintingService, PrintingService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ISyncService, SyncService>();
builder.Services.AddScoped<IRecieptService, RecieptService>();
builder.Services.AddScoped<ISMSService, SMSService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
