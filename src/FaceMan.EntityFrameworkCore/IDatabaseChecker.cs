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
}
