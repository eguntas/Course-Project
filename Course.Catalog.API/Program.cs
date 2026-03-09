using Course.Catalog.API;
using Course.Catalog.API.Features.Categories;
using Course.Catalog.API.Features.Courses;
using Course.Catalog.API.Options;
using Course.Catalog.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));
builder.Services.AddVersioningExtension();


var app = builder.Build();
app.AddSeedDataExtension().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data added successfully.");
});

app.AddCategoryEndpointExtension(app.AddVersionSetExtension());
app.AddCourseEndpointsExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();


