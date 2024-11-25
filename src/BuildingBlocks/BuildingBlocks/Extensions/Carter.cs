using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Extensions.Carter
{
    public static class CarterExtentions
    {
        // Because Carter is installed in this project and not on the API, it is not able to scan the correct assemblies
        // So, we need to create an extension method to add Carter with the correct assemblies
        public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                foreach (var assembly in assemblies)
                {
                    var modules = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

                    config.WithModules(modules);
                }
            });
            return services;
        }
    }
}

