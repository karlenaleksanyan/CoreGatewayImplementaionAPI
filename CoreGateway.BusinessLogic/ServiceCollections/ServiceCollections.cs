using CoreGateway.Abstraction;
using CoreGateway.BusinessLogic.Services;
using CoreGateway.Models.Models;
using CoreGateway.Repository.RepositoryCollections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreGateway.BusinessLogic.ServiceCollections
{
    public static class ServiceCollections
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IService<ProductModel>, ProductService>();
            services.AddScoped<IService<CategoryModel>, CategoryService>();
            services.AddScoped<IService<SupplierModel>, SupplierService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.ConfigureRepositories(Configuration);
        }
    }
}