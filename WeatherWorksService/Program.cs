using ClientLibrary.ExternalClients;
using Microsoft.OpenApi.Models;
using WeatherWorksService.Infrastructure.Middlewares;
using WeatherWorksService.Infrastructure.Swagger.OperationFilter;
using WeatherWorksService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddRequiredHeaderParameter>();
});

var apiKey = builder.Configuration["ExternalApi:OpenWeatherApi:ApiKey"];
var baseUrl = builder.Configuration["ExternalApi:OpenWeatherApi:BaseUrl"];

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
});

builder.Services.AddTransient<IOpenWeatherAPIClient>( _ => {
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri(baseUrl);

    return new OpenWeatherAPIClient(httpClient, apiKey);
});


builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Host.UseWindowsService();
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors("MyPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");

app.Run();
