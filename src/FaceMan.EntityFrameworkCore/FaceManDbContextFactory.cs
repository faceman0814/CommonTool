// Licensed to the .NET YoyoBoot under one or more agreements.
// The .NET YoyoBoot licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FaceMan.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FaceMan.EntityFrameworkCore
{
    public class FaceManDbContextFactory : IDesignTimeDbContextFactory<FaceManDbContext>
    {
        public FaceManDbContext CreateDbContext(string[] args)
        {
            // 设定配置文件路径
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var path = Path.Combine(directoryInfo.FullName, "FlyFramework.WebHost");
            Console.WriteLine("工作目录：{0}", path);
            // 配置 builder 来读取 appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();
            // 从配置中读取连接字符串
            var connectionString = configuration.GetConnectionString("Default");
            var databaseType = configuration.GetSection("ConnectionStrings:DatabaseType").Get<DatabaseType>();
            Console.WriteLine("迁移使用数据库连接字符串：{0}", connectionString);
            Console.WriteLine("迁移使用数据库类型：{0}", databaseType);
            var option = new DbContextOptionsBuilder<FaceManDbContext>();
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    option.UseSqlServer(connectionString);
                    break;
                case DatabaseType.MySql:
                    option.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31)));
                    break;

                case DatabaseType.Sqlite:
                    option.UseSqlite(connectionString);
                    break;

                case DatabaseType.Postgre:
                    option.UseNpgsql(connectionString);
                    break;

                default:
                    throw new Exception("不支持的数据库类型");
            }
            //获取appsettings配置
            return new FaceManDbContext(option.Options);
        }
    }
}
