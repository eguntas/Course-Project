using Course.Catalog.API;
using Course.Catalog.API.Features.Categories;
using Course.Catalog.API.Features.Courses;
using Course.Catalog.API.Options;
using Course.Catalog.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));
builder.Services.AddMasstransitCatalogExt(builder.Configuration);
builder.Services.AddVersioningExtension();

builder.Services.AddAuthenticationServiceExtension(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();
app.AddSeedDataExtension().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data added successfully.");
});

app.UseExceptionHandler(x => { });
app.AddCategoryEndpointExtension(app.AddVersionSetExtension());
app.AddCourseEndpointsExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.Run();


