
using GM.Store.Server.Config;
using GM.Store.Server.Database;
using GM.Store.Server.Helper;
using GM.Store.Server.Provider;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);
try
{

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

   
    var serviceConfig = builder.Configuration.GetSection("ServiceConfiguration").Get<ServiceConfiguration>();
    builder.Services.AddSingleton(serviceConfig);

    builder.Services.AddSingleton<IJwtAuthProvider, JwtAuthProvider>();
    builder.Services.AddSingleton<IDocumentStoreHolder, DocumentStoreHolder>();
    builder.Services.AddScoped<IEncodePasswordHelper, EncodePasswordHelper>();
    builder.Services.AddScoped<IDataAccessManager, DataAccessManager>();
    builder.Services.AddScoped<ISmsHelper, SmsHelper>();
    builder.Services.AddHttpClient();

    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serviceConfig.BaseApiUrl) });
    // Add services to the container.

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddSignalR();
    //builder.WebHost.UseKestrel(serverOptions =>
    //{
    //    serverOptions.ListenAnyIP(5000);
    //    //    serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps());
    //});

    builder.Services.AddSingleton<PeriodicHostedService>();
    builder.Services.AddHostedService(provider => provider.GetRequiredService<PeriodicHostedService>());


    var levelSwitch = new LoggingLevelSwitch();
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.ControlledBy(levelSwitch)
        .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
        .WriteTo.File(@"C:\logs\log.txt",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

    /* this is used instead of .UseSerilog to add Serilog to providers */
    builder.Logging.AddProvider(new SerilogLoggerProvider());

    var app = builder.Build();

 


    //Log.Logger = new LoggerConfiguration()
    //    .WriteTo.Logger(lc => lc
    //                    .Filter.ByIncludingOnly("")
    //                    .WriteTo.File("Logs/Log-Error-{Date}.log", rollingInterval: RollingInterval.Day))
    //    .CreateLogger();
  
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseCors(MyAllowSpecificOrigins);

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    Console.ReadKey();
}
