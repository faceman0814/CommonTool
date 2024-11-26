using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceMan.EntityFrameworkCore
{
    public static class DatabaseConfig
    {
        /// <summary>
        /// 跳过DbContext注册
        /// </summary>
        public static bool SkipDbContextRegistration { get; set; }

        /// <summary>
        /// 跳过种子数据
        /// </summary>
        public static bool SkipDbSeed { get; set; }
    }
}
