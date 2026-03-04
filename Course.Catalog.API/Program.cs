using Course.Catalog.API;
using Course.Catalog.API.Features.Categories;
using Course.Catalog.API.Features.Categories.Create;
using Course.Catalog.API.Options;
using Course.Catalog.API.Repositories;
using Course.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));



var app = builder.Build();
app.AddCategoryEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();


