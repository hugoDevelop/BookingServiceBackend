using BookingServiceBackend.Data;
using BookingServiceBackend.Middleware;
using BookingServiceBackend.Repositories;
using BookingServiceBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TokenValidationMiddleware>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddTransient<ClienteService>();
builder.Services.AddTransient<ServicioService>();
builder.Services.AddTransient<ReservaService>();

builder.Services.AddDbContext<BookingContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<TokenValidationMiddleware>();

app.MapControllers();

app.Run();
