using Microsoft.Data.Sqlite;

using System.Data;
using System.Diagnostics;



namespace FaceMan.Tools.Db
{
    public class DBHelper
    {
        static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() =>
        {
            var fsql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=..//..//..//local.db;")
                    .UseAutoSyncStructure(true)
                    .UseLazyLoading(true)
                    .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                    .Build();
            return fsql;
        });
        public static IFreeSql sqlite => sqliteLazy.Value;
    }
}
