using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FaceMan.EntityFrameworkCore
{
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
