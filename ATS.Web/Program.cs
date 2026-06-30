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


// =========================================
// ✅ REGISTER REPOSITORY LAYER COMPONENTS
// =========================================

// Core Repositories
builder.Services.AddScoped<IAdvertisementSourceRepository, AdvertisementSourceRepository>();
builder.Services.AddScoped<IHarverRepository, HarverRepository>();
builder.Services.AddScoped<IJobAdvertisementRepository, JobAdvertisementRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IRequisitionRequestRepository, RequisitionRequestRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<IGeoRepository, GeoRepository>();
builder.Services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
builder.Services.AddScoped<IInterviewStatusRepository, InterviewStatusRepository>();

// Applicant-related Repositories
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IApplicantPhoneNumberRepository, ApplicantPhoneNumberRepository>();
builder.Services.AddScoped<IApplicantEmailAddressRepository, ApplicantEmailAddressRepository>();
builder.Services.AddScoped<IApplicantAddressRepository, ApplicantAddressRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// Local Country-Specific Repositories
builder.Services.AddScoped<IJamaicaLocalRepository, JamaicaLocalRepository>();
builder.Services.AddScoped<IGuatemalaRepository, GuatemalaRepository>();
builder.Services.AddScoped<IHyderabadLocalRepository, HyderabadLocalRepository>();

// =========================================
// ✅ REGISTER SERVICE LAYER COMPONENTS
// =========================================

builder.Services.AddScoped<IAdvertisementSourceService, AdvertisementSourceService>();
builder.Services.AddScoped<IHarverService, HarverService>();
builder.Services.AddScoped<IJobAdvertisementService, JobAdvertisementService>();
builder.Services.AddScoped<ICandidateDetailService, CandidateDetailService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();

// Newly added skeleton services for Vue ATS App API endpoints
builder.Services.AddScoped<IApplicantSearchService, ApplicantSearchService>();
builder.Services.AddScoped<IApplicantDetailsService, ApplicantDetailsService>();
builder.Services.AddScoped<ICandidateSearchService, CandidateSearchService>();
builder.Services.AddScoped<ILookupsService, LookupsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Additional Service Dependencies
builder.Services.AddScoped<IDayforceRepository, DayforceRepository>();


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
