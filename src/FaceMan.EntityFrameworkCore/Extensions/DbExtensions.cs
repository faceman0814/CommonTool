using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceMan.EntityFrameworkCore.Extensions
{
    public static class DbExtensions
    {
        /// <summary>
        /// 获取Configuration
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IConfiguration GetConfiguration(this ServiceConfigerContext context)
        {
            if (context == null && context.Services is null) throw new ArgumentException("ServiceConfigerContext is null");
            return context.Provider.GetRequiredService<IConfiguration>();
        } 
        
     
    }

}
