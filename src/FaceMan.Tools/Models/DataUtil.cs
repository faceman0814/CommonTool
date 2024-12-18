using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceMan.Tools.Models
{
    public class DataUtil
    {
        public static readonly Dictionary<string, List<MenuItems>> MenuItems = new Dictionary<string, List<MenuItems>>()
        {
            { "代码生成器", new List<MenuItems>
                {
                    new MenuItems { Text = "实体列表" , Tag = "CodeEntitys"},
                }
            }
        };
    }
}
