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


// 2) Szolg�ltat�sok regisztr�l�sa
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

// 3) Swagger/OpenAPI regisztr�l�sa
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4) Middleware-l�nc
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Swagger JSON �s UI el�rhet�v� t�tele
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppointmentBooking API v1");
        c.RoutePrefix = string.Empty; // a gy�k�rre teszi a UI-t (https://localhost:5001/)
    });
}

app.UseHttpsRedirection();

// CORS middleware-t a routing el�tt kell h�vni
app.UseCors("AllowAngularDevClient");

app.MapControllers();

app.Run();
