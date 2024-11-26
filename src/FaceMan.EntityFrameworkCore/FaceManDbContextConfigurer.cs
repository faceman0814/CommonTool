using FaceMan.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FaceMan.EntityFrameworkCore
{
    public class FaceManDbContextConfigurer<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 根据配置信息注册数据库服务
        /// </summary>
        /// <param name="services"></param>
        public virtual void UsingDatabaseServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var databaseType = configuration.GetSection("ConnectionStrings:DatabaseType").Get<DatabaseType>();
            string connectionString = configuration.GetSection("ConnectionStrings:Default").Get<string>();
            Console.WriteLine($"数据库类型：{databaseType}");
            Console.WriteLine($"连接字符串：{connectionString}");
            services.AddDbContext<TContext>(option =>
            {
                new FaceManDbContextFactory<TContext>().ConfigureDatabase(option, databaseType, connectionString);
            });

            //注册IDatabaseChecker和IDbContextProvider
            services.AddScoped<IDbContextProvider, DbContextProvider<TContext>>();
            services.AddTransient<IDatabaseChecker, DatabaseChecker<TContext>>();
        }

        /// <summary>
        /// 数据库检查
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual bool PerformDatabaseCheck(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var databaseChecker = scope.ServiceProvider.GetRequiredService<IDatabaseChecker>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                string connectionString = configuration.GetConnectionString("Default");
                return databaseChecker.Exist(connectionString);
            }
        }

        /// <summary>
        /// 数据库种子数据
        /// </summary>
        public virtual void SeedData()
        {
            if (!DatabaseConfig.SkipDbSeed)
            {
                //TODO: 数据库种子数据
            }
        }
    }
}
