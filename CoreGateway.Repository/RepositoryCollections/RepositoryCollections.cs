using CoreGateway.DBMap;
using CoreGateway.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//using Microsoft.EntityFrameworkCore;

namespace CoreGateway.Repository.RepositoryCollections
{
    public static class RepositoryCollections
    {
        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<CoreGatewayDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoreGatewayConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}