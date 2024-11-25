using Microsoft.EntityFrameworkCore;

namespace FaceMan.EntityFrameworkCore
{
    public interface IDbContextProvider
    {
        DbContext GetDbContext();
    }
}
