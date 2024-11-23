using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceMan.DynamicWebAPI.ErrorExceptions
{
    /// <summary>
    /// 错误信息定义
    /// </summary>
    public enum ApiError
    {
        [Description("全局错误")]
        全局错误 = -1,
        [Description("默认错误")]
        默认 = 1,
        [Description("参数错误")]
        参数错误 = 2,


        [Description("登录错误")]
        登录错误 = 1000,
        [Description("鉴权错误")]
        鉴权错误 = 1001,


        [Description("权限错误")]
        无权限 = 3000,

        [Description("外部接口错误")]
        外部接口错误 = 4000,
        [Description("外部接口状态码错误")]
        外部接口状态码错误 = 4001,

        [Description("数据格式化错误")]
        数据格式化错误 = 5000,

        [Description("对象为Null")]
        对象为NULL = 6000,

    }
}
