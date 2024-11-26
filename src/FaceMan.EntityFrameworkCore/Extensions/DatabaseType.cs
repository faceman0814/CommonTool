using System.ComponentModel;

namespace FaceMan.EntityFrameworkCore.Extensions
{
    public enum DatabaseType
    {
        [Description("SQLServer")]
        SqlServer,
        [Description("MySql")]
        MySql,
        [Description("Postgre")]
        Postgre,
        [Description("Oracle")]
        Oracle,
        [Description("SQLite")]
        Sqlite
    }
}