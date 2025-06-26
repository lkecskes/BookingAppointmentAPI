using AppointmentBooking.Application;
using AppointmentBooking.Infrastructure;
using AppointmentBooking.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);
{
    // Application réteg
    builder.Services.AddApplicationServices();

    // DbContext, repositoryk
    builder.Services.AddPersistenceServices(builder.Configuration);

    // Third party services, pl EmailService, FileStorage, stb.
    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}




