using Course.Payment.API;
using Course.Payment.API.Features.Payments;
using Course.Payment.API.Repositories;
using Course.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExtension();
builder.Services.AddCommonServiceExtension(typeof(PaymentAssembly));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});
builder.Services.AddAuthenticationServiceExtension(builder.Configuration);

var app = builder.Build();
app.AddPaymentEndpointExtension(app.AddVersionSetExtension());


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


