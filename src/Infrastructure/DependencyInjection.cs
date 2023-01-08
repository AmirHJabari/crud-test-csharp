using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Infrastructure.Models;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CrudTest"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddSettingModels(configuration);
        return services;
    }

    /// <summary>
    /// Registers the interfaces that has inherite <see cref="IIsSettingModel"/>.
    /// </summary>
    public static void AddSettingModels(this IServiceCollection services, IConfiguration configuration)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type isSettingType = typeof(IIsSettingModel);

        var settingInterfaces = assembly.GetTypes().Where(t =>
                t.IsInterface &&
                t.GetInterfaces().Any(i => i.IsInterface && i == isSettingType)).ToList();

        var settingClasses = assembly.GetTypes().Where(t =>
                t.IsClass &&
                t.GetInterfaces().Any(i => i.IsInterface && settingInterfaces.Any(ii => ii == i))).ToList();

        foreach (var @interface in settingInterfaces)
        {
            var @class = settingClasses.First(c => c.GetInterfaces().Any(i => i.IsInterface && i == @interface));

            var obj = configuration.GetSection($"Settings:{@class.Name}").Get(@class);

            services.AddSingleton(@interface, obj);
        }
    }
}
