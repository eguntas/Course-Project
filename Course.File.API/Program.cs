using Course.File.API;
using Course.File.API.Features.File;
using Course.Shared.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExtension();
builder.Services.AddCommonServiceExtension(typeof(FileAssembly));
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

builder.Services.AddAuthenticationServiceExtension(builder.Configuration);

var app = builder.Build();
app.AddFileEndpointExtension(app.AddVersionSetExtension());
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Run();


