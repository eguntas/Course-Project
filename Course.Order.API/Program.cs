using Course.Bus;
using Course.Order.API.Endpoints;
using Course.Order.Application;
using Course.Order.Application.BackgroundServices;
using Course.Order.Application.Contracts.Refit;
using Course.Order.Application.Contracts.Refit.PaymentService;
using Course.Order.Application.Contracts.Repositories;
using Course.Order.Application.Contracts.UnitOfWorks;
using Course.Order.Persistence;
using Course.Order.Persistence.Repositories;
using Course.Order.Persistence.UnitOfWork;
using Course.Shared.Extensions;
using Course.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(OrderApplicationAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);



builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddVersioningExtension();
builder.Services.AddVersioningExtension();
builder.Services.AddRefitConfigurationExt(builder.Configuration);
builder.Services.AddHostedService<CheckPaymentStatusOrderBackgroundService>();

builder.Services.AddAuthenticationServiceExtension(builder.Configuration);

var app = builder.Build();

app.AddOrderEndpointsExtension(app.AddVersionSetExtension());

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

