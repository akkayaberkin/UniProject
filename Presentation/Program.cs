using KocUniversityCourseManagement.Application.Interfaces;
using KocUniversityCourseManagement.Application.Services;
using KocUniversityCourseManagement.Infrastructure;
using KocUniversityCourseManagement.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost", 
        UserName = "user",      
        Password = "password"  
    };
    return factory.CreateConnection();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookie";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookie")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "http://localhost:8080/auth/realms/MyUniversityRealm";
    options.ClientId = "CourseManagementWebApp";
    options.ClientSecret = "ClientSecret123";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.RequireHttpsMetadata = false;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<RateLimitingMiddleware>();
app.MapControllers();

app.Run();
