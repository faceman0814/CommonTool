// Licensed to the .NET under one or more agreements.
// The .NET licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FaceMan.EntityFrameworkCore
{
    public interface IDatabaseChecker
    {
        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        bool Exist(string connectionString);

        /// <summary>
        /// 获取当前的数据库上下文实例
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
    }

    public interface IDatabaseChecker<TDbContext> : IDatabaseChecker
        where TDbContext : DbContext
    {

    }


    public class DatabaseChecker<TDbContext> : IDatabaseChecker<TDbContext>
         where TDbContext : DbContext
    {
        private readonly IDbContextProvider _dbContextProvider;

        public DatabaseChecker(
            IDbContextProvider dbContextProvider
        )
        {
            _dbContextProvider = dbContextProvider;
        }

        public bool Exist(string connectionString)
        {
            if (connectionString.IsNullOrEmpty())
            {
                //单元测试下连接字符串为空
                return true;
            }

            try
            {
                Console.WriteLine($"数据库连接字符串:{connectionString}");
                _dbContextProvider.GetDbContext().Database.OpenConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库连接字符串错误:{ex.Message}");
                throw new ArgumentException($"数据库连接字符串错误:{ex.Message}");
            }

            //如果能打开连接，说明数据库存在，释放连接
            _dbContextProvider.GetDbContext().Database.CloseConnection();
            return true;
        }

        public virtual DbContext GetDbContext()
        {
            return _dbContextProvider.GetDbContext();
        }
    }
}
