using Course.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddAuthenticationServiceExtension(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapReverseProxy();

app.UseExceptionHandler(x => { });
app.MapGet("/", () => "YARP Gateway!");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
