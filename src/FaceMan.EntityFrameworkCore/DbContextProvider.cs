using Microsoft.EntityFrameworkCore;

namespace FaceMan.EntityFrameworkCore
{
    public class DbContextProvider<TContext> : IDbContextProvider where TContext : DbContext
    {
        private readonly TContext _context;

        public DbContextProvider(TContext context)
        {
            _context = context;
        }

        public DbContext GetDbContext()
        {
            return _context;
        }
    }
}
