using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Repositories;
using Stereograph.TechnicalTest.Api.Utils;
using System;

namespace Stereograph.TechnicalTest.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlite("Data Source=testtechnique.db")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        services.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stereograph.TechTechnique.Api", Version = "v1" });
            options.CustomSchemaIds(schema => schema.FullName);
        });

        services
            .AddControllers();

        services.AddScoped<ApplicationDbSeeder>();

        services.AddScoped<IRepository<Person>, PersonRepository>();
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            application
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stereograph.TechTechnique.Api V1"));
        }

        application
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors()
            .UseEndpoints(endpoints => endpoints.MapControllers());


        using IServiceScope scope = application.ApplicationServices.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        ApplicationDbContext appDbContext = services.GetRequiredService<ApplicationDbContext>();
        appDbContext.Database.Migrate();
    }
}
