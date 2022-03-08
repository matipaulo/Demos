using MinimalAPIs.Endpoints;
using MinimalAPIs.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<IEndpointRouting, WeatherForecastEndpoints>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var apis = app.Services.GetServices<IEndpointRouting>();
foreach (var api in apis)
{
    if (api is null) throw new InvalidProgramException("Apis not found");

    api.Register(app);
}

app.Run();