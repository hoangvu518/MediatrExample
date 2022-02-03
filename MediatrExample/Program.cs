using FluentValidation.AspNetCore;
using MediatR;
using MediatrExample.Core.Services;
using MediatrExample.Infrastructure;
using MediatrExample.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type =>
    {
        var typeName = type.FullName.Replace("+", "");
        var featureIndex = typeName.IndexOf("Features");
        if (featureIndex > 0)
        {
            var removedEndIndex = featureIndex + "Features".Length + 1;
            var modelName = typeName.Remove(0, removedEndIndex);
            return modelName;
        }
        return typeName;
    });
    //options.CustomSchemaIds(type => type.FullName.Replace("+", "_"));
    //options.CustomSchemaIds(type => type.ToString());
    //options.CustomSchemaIds(type =>
    //{
    //    var types = type.ToString().Split(".");
    //    var myType = types.Last();
    //    return myType;
    //});
});
builder.Services.AddMvc()
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddDbContext<AppDb>(options =>
    {
        if (environment.IsDevelopment())
        {
            options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"))
                .EnableSensitiveDataLogging(); ;
        }
        else
        {
            options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
        }
    }
);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
