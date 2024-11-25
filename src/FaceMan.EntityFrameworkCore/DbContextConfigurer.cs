using FaceMan.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FaceMan.EntityFrameworkCore
{
    public static class DbContextConfigurer
    {
        public static void UsingDatabaseServices(IServiceCollection services, ConfigurationManager configuration)
        {
            var databaseType = configuration.GetSection("ConnectionStrings:DatabaseType").Get<DatabaseType>();
            string connectionString = configuration.GetSection("ConnectionStrings:Default").Get<string>();
            Console.WriteLine($"数据库类型：{databaseType}");
            Console.WriteLine($"连接字符串：{connectionString}");
            services.AddDbContext<FaceManDbContext>(option =>
            {
                switch (databaseType)
                {
                    case DatabaseType.SqlServer:
                        option.UseSqlServer(connectionString);
                        //.AddInterceptors(new FlyFrameworkEFCoreInterceptor());
                        break;

                    //case DatabaseType.MySql:
                    //    option.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)));
                    //    break;

                    case DatabaseType.Sqlite:
                        option.UseSqlite(connectionString);
                        break;

                    case DatabaseType.Postgre:
                        option.UseNpgsql(connectionString);
                        break;

                    //case DatabaseType.Oracle:
                    //    option.UseOracle(connectionString, (config) =>
                    //    {
                    //        config.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23);
                    //        config.MigrationsHistoryTable("__EFMIGRATIONHISTORY");
                    //    }).AddInterceptors(new CommentCommandInterceptor())
                    //        //.UseRivenOracleTypeMapping()
                    //        ;
                    //    //if (DatabaseInfo.Instance.DatabaseType == DatabaseTypeEnum.Oracle)
                    //    //{
                    //    //    Configuration.UnitOfWork.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    //    //}
                    //    break;

                    default:
                        throw new Exception("不支持的数据库类型");
                }
            });
        }
    }
}
