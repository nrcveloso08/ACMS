using ATS.Data;
using ATS.Data.Repository;
using ATS.Service;
using ATS.Service.WebServiceHelper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// =========================================
// 1️⃣ CONFIGURATION: Load from appsettings.json
// =========================================
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings")
);

// =========================================
// 2️⃣ DEPENDENCY INJECTION SETUP
// =========================================
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// ✅ Register WebServiceHelper using the BaseUrl from appsettings.json
builder.Services.AddSingleton<IWebServiceHelper>(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;

    if (string.IsNullOrWhiteSpace(apiSettings.BaseUrl))
    {
        throw new InvalidOperationException("❌ Missing configuration: ApiSettings:BaseUrl in appsettings.json");
    }

    Console.WriteLine($"🌐 API Base URL Loaded: {apiSettings.BaseUrl}");
    return new WebServiceHelper(apiSettings.BaseUrl);
});

// ✅ Initialize KITT API instance
var configuration = builder.Configuration;
var kittBaseUrl = configuration["ApiSettings:KittBaseUrl"];
var kittToken = configuration["ApiSettings:KittToken"];

if (!string.IsNullOrWhiteSpace(kittBaseUrl))
{
    WebServiceHelper.InitializeKitt(kittBaseUrl, token: kittToken);
    Console.WriteLine($"🤖 KITT API Base URL Loaded: {kittBaseUrl}");
}
else
{
    Console.WriteLine("⚠️ Warning: ApiSettings:KittBaseUrl not configured.");
}


// ✅ Register repository layer components
builder.Services.AddScoped<IAdvertisementSourceRepository, AdvertisementSourceRepository>();
builder.Services.AddScoped<IHarverRepository, HarverRepository>();
builder.Services.AddScoped<IJobAdvertisementRepository, JobAdvertisementRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository> ();
builder.Services.AddScoped<IRequisitionRequestService, RequisitionRequestRepository> ();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository> ();

// ✅ Register business/service layer components
builder.Services.AddScoped<IAdvertisementSourceService, AdvertisementSourceService>();
builder.Services.AddScoped<IHarverService, HarverService>();
builder.Services.AddScoped<IJobAdvertisementService, JobAdvertisementService>();

// =========================================
// 3️⃣ BUILD THE APPLICATION
// =========================================
var app = builder.Build();

// =========================================
// 4️⃣ MIDDLEWARE PIPELINE
// =========================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}"
);

// =========================================
// 5️⃣ RUN
// =========================================
app.Run();
