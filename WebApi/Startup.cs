using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WebApi.Extensions;

namespace WebApi;

public class Startup
{
    public const string PublicPolicy = "public_policy";
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger();

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddEndpointsApiExplorer();
        
        #region CORS
        services.AddCors();
        services.AddCors((options) =>
        {
            options.AddPolicy(PublicPolicy, (builder) =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        #endregion

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
            app.UseSwaggerAndSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseCors(PublicPolicy);

        app.UseRouting();
        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
