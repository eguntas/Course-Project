using Course.Discount.API;
using Course.Discount.API.Features.Discounts;
using Course.Discount.API.Options;
using Course.Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExtension();
builder.Services.AddOptionExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServiceExtension(typeof(DiscountAssembly));

builder.Services.AddAuthenticationServiceExtension(builder.Configuration);



var app = builder.Build();

app.AddDiscountEndpointExtension(app.AddVersionSetExtension());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();

app.Run();

