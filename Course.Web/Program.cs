using Course.Web.DelegateHandlers;
using Course.Web.ExceptionHandlers;
using Course.Web.Extensions;
using Course.Web.Options;
using Course.Web.Pages.Auth.SignIn;
using Course.Web.Pages.Auth.SignUp;
using Course.Web.Services;
using Course.Web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOptionExt();
builder.Services.AddMvc(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<AuthenticatedHttpClientHandler>();
builder.Services.AddScoped<ClientAuthenticatedHttpClientHandler>();
builder.Services.AddScoped<UserService>();
builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();



builder.Services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(c =>
{
    var gatewayOption = builder.Configuration.GetSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();
    c.BaseAddress = new Uri(gatewayOption!.Catalog.BaseAddress);
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
.AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();




builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Auth/SignIn";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.Cookie.Name = "CourseWebAuthCookie";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

builder.Services.AddAuthentication();


var app = builder.Build();

var cultureInfo = new System.Globalization.CultureInfo("en-US");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
    SupportedCultures = [ cultureInfo ],
    SupportedUICultures = [cultureInfo]
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
