using EventBus.Messages;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Consumers;
using Ordering.Api.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(options => options.UseInMemoryDatabase(databaseName: "OrderDB"));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<CheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckout, c =>
        {
            c.ConfigureConsumer<CheckoutConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();