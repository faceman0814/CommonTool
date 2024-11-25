using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System.Linq.Expressions;
using System.Reflection;
namespace FaceMan.EntityFrameworkCore
{
    public class FaceManDbContext : DbContext
    {
        public FaceManDbContext(DbContextOptions<FaceManDbContext> options) : base(options)
        {
        }

        public FaceManDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> Test { get; set; }

        //需要显示调用时注册
        //public DbSet<UserRole> UserRole { get; set; }
    }
}
