using Course.Basket.API;
using Course.Basket.API.Feature.Basket;
using Course.Bus;
using Course.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(BasketAssembly));
builder.Services.AddMasstransitBasketExt(builder.Configuration);
builder.Services.AddScoped<BasketService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddAuthenticationServiceExtension(builder.Configuration);
builder.Services.AddVersioningExtension();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddBasketEndpointExtension(app.AddVersionSetExtension());

app.UseAuthentication();
app.UseAuthorization();
app.Run();


