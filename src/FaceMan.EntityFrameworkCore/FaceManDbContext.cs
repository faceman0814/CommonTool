using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System.Linq.Expressions;
using System.Reflection;
namespace FaceMan.EntityFrameworkCore
{
    public partial class FaceManDbContext : DbContext
    {

        public FaceManDbContext(DbContextOptions<FaceManDbContext> options)
        {
        }

        //需要显示调用时注册
        //public DbSet<UserRole> UserRole { get; set; }
    }
}
