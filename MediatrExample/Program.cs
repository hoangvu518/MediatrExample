using FluentValidation.AspNetCore;
using MediatR;
using MediatrExample.Core.Services;
using MediatrExample.Infrastructure;
using MediatrExample.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostbuilderContext, loggerConfiguration) => loggerConfiguration.MinimumLevel.Information()
                                                                                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                                                                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)

                                                                                         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                                                                         .Enrich.FromLogContext()
                                                                                         .Enrich.WithEnvironmentName()
                                                                                         .Enrich.WithMachineName()
                                                                                        .WriteTo.Console()
                                                                                        .WriteTo.File(new CompactJsonFormatter(), "log.text")
                                                                                        ); ;
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SecurityAuditLogService>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
//builder.Services.AddHttpLogging(options => // <--- Setup logging
//{
//    // Specify all that you need here:
//    options.LoggingFields = HttpLoggingFields.RequestHeaders |
//                            HttpLoggingFields.RequestBody |
//                            HttpLoggingFields.ResponseHeaders |
//                            HttpLoggingFields.ResponseBody;
//});
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

//app.UseHttpLogging();
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<EnableRequestBodyBufferingMiddleware>();
app.UseMiddleware<RequestResponseLoggerMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
//app.UseMiddleware<SecurityAuditLogMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { } //this line is for testing in .net 6. See https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0#test-with-webapplicationfactory-or-testserver