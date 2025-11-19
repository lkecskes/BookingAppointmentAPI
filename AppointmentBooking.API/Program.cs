using AppointmentBooking.Application;
using AppointmentBooking.Application.Common.Settings;
using AppointmentBooking.Application.Services.Authentication;
using AppointmentBooking.Infrastructure;
using AppointmentBooking.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings!);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtService, JwtService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret!)),
            ClockSkew = TimeSpan.Zero
        };
    });

// 1) CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


// 2) Szolgáltatások regisztrálása
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4) Middleware-lánc
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Swagger JSON és UI elérhetõvé tétele
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppointmentBooking API v1");
        c.RoutePrefix = string.Empty; // a gyökérre teszi a UI-t (https://localhost:5001/)
    });
}

app.UseHttpsRedirection();

// CORS middleware-t a routing elõtt kell hívni
app.UseCors("AllowAngularDevClient");

// Add authentication & authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
