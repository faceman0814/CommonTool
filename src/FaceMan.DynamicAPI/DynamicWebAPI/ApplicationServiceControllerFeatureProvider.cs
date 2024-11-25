﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FaceMan.DynamicWebAPI
{
    /// <summary>
    /// 自定义控制器特性提供程序，用于将实现了 IApplicationService 接口的类识别为控制器。
    /// </summary>
    public class ApplicationServiceControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        /// <inheritdoc />
        public void PopulateFeature(
            IEnumerable<ApplicationPart> parts,
            ControllerFeature feature)
        {
            foreach (var part in parts.OfType<IApplicationPartTypeProvider>())
            {
                foreach (var type in part.Types)
                {
                    if (IsController(type) && !feature.Controllers.Contains(type))
                    {
                        feature.Controllers.Add(type);
                    }
                }
            }
        }
        /// <summary>
        /// 判断给定的类型是否为控制器。
        /// </summary>
        /// <param name="typeInfo">要判断的类型信息。</param>
        /// <returns>如果类型是控制器，则返回 true；否则返回 false。</returns>
        protected virtual bool IsController(TypeInfo typeInfo)
        {
            // 如果具有NonControllerAttribute，则直接返回false
            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }
            // 获取实际的Type
            var type = typeInfo.AsType();

            // 检查类型是否为公开的、非抽象的、非泛型的、非接口的，并满足特定继承或属性条件
            if (typeInfo.IsPublic && !typeInfo.IsAbstract && !typeInfo.IsGenericType && !typeInfo.IsInterface)
            {
                // 检查类型是否继承自IApplicationService，或标记了DynamicWebApiAttribute，或继承自ControllerBase或Controller
                bool isAssignableToIApplicationService = typeof(IApplicationService).IsAssignableFrom(type);
                bool hasDynamicWebApiAttribute = type.IsDefined(typeof(DynamicWebApiAttribute), true);
                bool isDerivedFromControllerBase = type.BaseType == typeof(ControllerBase);
                bool isDerivedFromController = type.BaseType == typeof(Controller);

                // 任一条件满足即视为Controller
                if (isAssignableToIApplicationService || hasDynamicWebApiAttribute || isDerivedFromControllerBase || isDerivedFromController)
                {
                    return true;
                }
            }

            return false;

        }
    }
}
