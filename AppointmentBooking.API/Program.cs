using AppointmentBooking.Application;
using AppointmentBooking.Infrastructure;
using AppointmentBooking.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

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

// 3) Swagger/OpenAPI regisztrálása
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

app.MapControllers();

app.Run();
