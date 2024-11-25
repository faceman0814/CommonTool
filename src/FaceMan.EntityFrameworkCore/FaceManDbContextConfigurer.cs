using FaceMan.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FaceMan.EntityFrameworkCore
{
    public class FaceManDbContextConfigurer<TContext> where TContext : DbContext
    {
        public virtual void UsingDatabaseServices(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var databaseType = configuration.GetSection("ConnectionStrings:DatabaseType").Get<DatabaseType>();
            string connectionString = configuration.GetSection("ConnectionStrings:Default").Get<string>();
            Console.WriteLine($"数据库类型：{databaseType}");
            Console.WriteLine($"连接字符串：{connectionString}");
            services.AddDbContext<TContext>(option =>
            {
                new FaceManDbContextFactory<TContext>().ConfigureDatabase(option, databaseType, connectionString);
            });
        }
    }
}
