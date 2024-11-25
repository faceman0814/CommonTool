// Licensed to the .NET YoyoBoot under one or more agreements.
// The .NET YoyoBoot licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FaceMan.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FaceMan.EntityFrameworkCore
{
    public class FaceManDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public virtual TContext CreateDbContext(string[] args)
        {
            var hostName = args[0];
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var path = Path.Combine(directoryInfo.FullName, hostName);

            Console.WriteLine("工作目录：{0}", path);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Default");
            var databaseType = configuration.GetSection("ConnectionStrings:DatabaseType").Get<DatabaseType>();
            Console.WriteLine("迁移使用数据库连接字符串：{0}", connectionString);
            Console.WriteLine("迁移使用数据库类型：{0}", databaseType);


            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case DatabaseType.Sqlite:
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                case DatabaseType.Postgre:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
        }
    }
}
