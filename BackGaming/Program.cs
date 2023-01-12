using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyAllowSpecificOrigins",
//                      policy =>
//                      {
//                          policy.WithOrigins("https://localhost:4200", "https://localhost:7256")
//                                .AllowAnyHeader()
//                                .AllowAnyMethod(); ;
//                      });
//});
// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GamingApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GamingApiConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
               builder.WithOrigins("*")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
           );
app.UseAuthorization();

app.MapControllers();

app.Run();
