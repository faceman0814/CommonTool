## 功能
快速集成EntityFrameWorkCore
- 支持SqlServer，Postgre、SQLite(Oracle、Mysql暂时不支持.net9后续有更新的话会加上)
- 根据appsettings.json配置动态注入数据库

## 快速开始

nuget引入包FaceMan.EntityFrameworkCore

### 数据库优先
Scaffold-DbContext "你的连接字符串" 数据库包的名字 -OutputDir 输出的文件夹名称
例如：
```shell
Scaffold-DbContext "Host=xxxx;Port=5432;Database=Demo;Username=postgres;Password=bb123456" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models
```

### 代码优先


创建DemoDbContext
```csharp
    public class DemoDbContext : FaceManDbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }


        public DbSet<User> User { get; set; }
    }
```
创建DemoDbContextFactory工厂类
```csharp
 public class DemoDbContextFactory : FaceManDbContextFactory<DemoDbContext>
    {
        public override DemoDbContext CreateDbContext(string[] args)
        {
            //这里需要传入appsettings所在项目的名字
            args = new[] { "Web项目名称" }.Concat(args).ToArray();
            return base.CreateDbContext(args);
        }
    }
```
配置appsettings.json
```Json
 "ConnectionStrings": {
    "DatabaseType": "Postgre",//详情请看DatabaseType
    "Default": "Host=xxxxx;Port=5432;Database=Demo;Username=postgres;Password=bb123456"
  },
```
EF所在项目执行迁移命令
```shell
//生成迁移文件
Add-Migration Init
//执行到数据库
Update-DataBase
//撤回迁移文件-不需要不执行
Remove-Migration
```

### 注册
创建DbExtensions
```csharp
public static class DbExtensions
    {
        /// <summary>
        /// 注册数据库服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDatabase(this IServiceCollection services)
        {
            var configurer = new FaceManDbContextConfigurer<DemoDbContext>();
            //注册数据库
            configurer.UsingDatabaseServices(services);
            //检查数据库
            var isDatabaseExists = configurer.PerformDatabaseCheck(services.BuildServiceProvider());
            //如果数据库存在，则执行种子数据
            if (isDatabaseExists)
            {
                configurer.SeedData();
            }
        }
    }
```
Program或者StartUp注册
```csharp
builder.Services.RegisterDatabase();
```

