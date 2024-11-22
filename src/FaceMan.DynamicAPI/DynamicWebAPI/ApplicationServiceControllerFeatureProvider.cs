using Microsoft.AspNetCore.Mvc.Controllers;

using System.Reflection;
using System.Web.Mvc;

namespace FaceMan.DynamicWebAPI
{
    /// <summary>
    /// 自定义控制器特性提供程序，用于将实现了 IApplicationService 接口的类识别为控制器。
    /// </summary>
    public class ApplicationServiceControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 判断给定的类型是否为控制器。
        /// </summary>
        /// <param name="typeInfo">要判断的类型信息。</param>
        /// <returns>如果类型是控制器，则返回 true；否则返回 false。</returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            // 检查类型是否满足以下条件：
            var type = typeInfo.AsType();
            if ((typeof(IApplicationService).IsAssignableFrom(type) || //判断是否继承ICoreDynamicController接口
                type.IsDefined(typeof(DynamicWebApiAttribute), true) ||// 判断是否标记了DynamicAPIAttribute特性
                 type.BaseType == typeof(Controller)) &&
                typeInfo.IsPublic && !typeInfo.IsAbstract && !typeInfo.IsGenericType && !typeInfo.IsInterface)//必须是Public、不能是抽象类、必须是非泛型的
            {
                return true;
            }

            // 如果不满足上述条件，则返回 false
            return false;
        }
    }
}
