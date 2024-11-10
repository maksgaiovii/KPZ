using Microsoft.EntityFrameworkCore;
using KPZ_lab5.Models;
using KPZ_lab5.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using KPZ_lab5;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Заміна MySQL на PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)); // Використовуємо PostgreSQL замість MySQL

// Configure CORS to allow requests from http://localhost:8000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8000", builder =>
        builder.WithOrigins("http://localhost:8000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply the CORS policy
app.UseCors("AllowLocalhost8000");

app.UseAuthorization();

app.MapControllers();

app.Run();