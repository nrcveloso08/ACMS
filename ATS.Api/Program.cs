using ATS.Data;
using ATS.Data.Repository;
using ATS.Service;
using ATS.Service.WebServiceHelper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// =========================================
// CONFIGURATION: Load from appsettings.json
// =========================================
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings")
);

// =========================================
// SERVICES
// =========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// CORS for Vue dev server
const string VueDevCorsPolicy = "VueDevCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(VueDevCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register WebServiceHelper using the BaseUrl from appsettings.json
builder.Services.AddSingleton<IWebServiceHelper>(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    var logger = sp.GetRequiredService<ILogger<WebServiceHelper>>();

    if (string.IsNullOrWhiteSpace(apiSettings.BaseUrl))
    {
        throw new InvalidOperationException("Missing configuration: ApiSettings:BaseUrl in appsettings.json");
    }

    return new WebServiceHelper(
        apiSettings.BaseUrl,
        timeoutSeconds: apiSettings.TimeoutSeconds,
        maxRetries: apiSettings.RetryCount,
        logger: logger);
});

// HttpClientFactory used by health checks
builder.Services.AddHttpClient();

// Health check for the legacy ATSWebService dependency
builder.Services.AddHealthChecks()
    .AddCheck<ATS.Api.HealthChecks.AtsWebServiceHealthCheck>("atswebservice");

// Initialize KITT API instance
var kittBaseUrl = builder.Configuration["ApiSettings:KittBaseUrl"];
var kittToken = builder.Configuration["ApiSettings:KittToken"];
var apiTimeoutSeconds = builder.Configuration.GetValue<int?>("ApiSettings:TimeoutSeconds") ?? 30;
var apiRetryCount = builder.Configuration.GetValue<int?>("ApiSettings:RetryCount") ?? 2;

if (!string.IsNullOrWhiteSpace(kittBaseUrl))
{
    WebServiceHelper.InitializeKitt(
        kittBaseUrl,
        token: kittToken ?? string.Empty,
        timeoutSeconds: apiTimeoutSeconds,
        maxRetries: apiRetryCount);
}

// =========================================
// REPOSITORY LAYER
// =========================================
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

builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IApplicantPhoneNumberRepository, ApplicantPhoneNumberRepository>();
builder.Services.AddScoped<IApplicantEmailAddressRepository, ApplicantEmailAddressRepository>();
builder.Services.AddScoped<IApplicantAddressRepository, ApplicantAddressRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IApplicantStatusRepository, ApplicantStatusRepository>();
builder.Services.AddScoped<IApplicantInterviewStatusRepository, ApplicantInterviewStatusRepository>();
builder.Services.AddScoped<IEmploymentApplicationRepository, EmploymentApplicationRepository>();

builder.Services.AddScoped<IJamaicaLocalRepository, JamaicaLocalRepository>();
builder.Services.AddScoped<IGuatemalaRepository, GuatemalaRepository>();
builder.Services.AddScoped<IHyderabadLocalRepository, HyderabadLocalRepository>();
builder.Services.AddScoped<IDayforceRepository, DayforceRepository>();
builder.Services.AddScoped<IEgyptAdditionalInfoRepository, EgyptAdditionalInfoRepository>();
builder.Services.AddScoped<IManilaLocalRepository, ManilaLocalRepository>();
builder.Services.AddScoped<IMexicoLocalRepository, MexicoLocalRepository>();

builder.Services.AddScoped<IApplicantSkillRepository, ApplicantSkillRepository>();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IMedicalResultRepository, MedicalResultRepository>();
builder.Services.AddScoped<IPrescreeningQuestionsRepository, PrescreeningQuestionsRepository>();
builder.Services.AddScoped<IPrescreeningQuestionGroupsRepository, PrescreeningQuestionGroupsRepository>();

builder.Services.AddScoped<IRosterRepository, RosterRepository>();
builder.Services.AddScoped<IRosterCSIRepository, RosterCSIRepository>();
builder.Services.AddScoped<IRosterReconciliationRepository, RosterReconciliationRepository>();
builder.Services.AddScoped<IObeyaBoardWaveRepository, ObeyaBoardWaveRepository>();
builder.Services.AddScoped<IStatusActionMapRepository, StatusActionMapRepository>();
builder.Services.AddScoped<IATSEmailTemplateRepository, ATSEmailTemplateRepository>();
builder.Services.AddScoped<ISMSTemplateRepository, SMSTemplateRepository>();
builder.Services.AddScoped<ISMSNumberRepository, SMSNumberRepository>();

builder.Services.AddScoped<IReportingRepository, ReportingRepository>();
builder.Services.AddScoped<IJobResponseRepository, JobResponseRepository>();
builder.Services.AddScoped<IFurstPersonRepository, FurstPersonRepository>();

builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewTypeRepository, InterviewTypeRepository>();
builder.Services.AddScoped<IFCRARepository, FCRARepository>();
builder.Services.AddScoped<IFCRADocumentRepository, FCRADocumentRepository>();
builder.Services.AddScoped<IFrontApiRepository, FrontApiRepository>();

builder.Services.AddScoped<IHRRepository, HRRepository>();
builder.Services.AddScoped<IFollowUpsRepository, FollowUpsRepository>();
builder.Services.AddScoped<IBogotaLocalRepository, BogotaLocalRepository>();
builder.Services.AddScoped<IEmployeeStatusRepository, EmployeeStatusRepository>();
builder.Services.AddScoped<IGuatemalaLocalRepository, GuatemalaLocalRepository>();
builder.Services.AddScoped<IHondurasLocalRepository, HondurasLocalRepository>();
builder.Services.AddScoped<IHiringSourceRepository, HiringSourceRepository>();

// =========================================
// SERVICE LAYER
// =========================================
builder.Services.AddScoped<IAdvertisementSourceService, AdvertisementSourceService>();
builder.Services.AddScoped<IHarverService, HarverService>();
builder.Services.AddScoped<IJobAdvertisementService, JobAdvertisementService>();
builder.Services.AddScoped<ICandidateDetailService, CandidateDetailService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();

builder.Services.AddScoped<IApplicantSearchService, ApplicantSearchService>();
builder.Services.AddScoped<IApplicantDetailsService, ApplicantDetailsService>();
builder.Services.AddScoped<ICandidateSearchService, CandidateSearchService>();
builder.Services.AddScoped<ILookupsService, LookupsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<IInterviewScheduleService, InterviewScheduleService>();
builder.Services.AddScoped<ICsiScheduleService, CsiScheduleService>();
builder.Services.AddScoped<IWaveRosterService, WaveRosterService>();
builder.Services.AddScoped<IObeyaBoardService, ObeyaBoardService>();
builder.Services.AddScoped<IHRProcessingService, HRProcessingService>();
builder.Services.AddScoped<IFollowUpManagementService, FollowUpManagementService>();
builder.Services.AddScoped<IJobResponsePageService, JobResponsePageService>();
builder.Services.AddScoped<IATSConfigService, ATSConfigService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<ISkillTestService, SkillTestService>();
builder.Services.AddScoped<IMedicalService, MedicalService>();
builder.Services.AddScoped<IFCRAService, FCRAService>();
builder.Services.AddScoped<IDayforceService, DayforceService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IFrontService, FrontService>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IRequisitionService, RequisitionService>();

var app = builder.Build();

// =========================================
// MIDDLEWARE PIPELINE
// =========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(VueDevCorsPolicy);

app.UseAuthorization();

app.MapControllers();

// Simple health endpoint indicating ATSWebService connectivity status
app.MapHealthChecks("/health/atswebservice");

app.Run();
