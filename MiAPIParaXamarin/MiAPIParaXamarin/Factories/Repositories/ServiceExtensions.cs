using MiAPIParaXamarin.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MiAPIParaXamarin.Factories.Repositories
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
        }
    }
}
