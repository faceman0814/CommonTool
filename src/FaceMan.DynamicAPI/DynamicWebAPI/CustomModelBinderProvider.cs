using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;

namespace FaceMan.DynamicWebAPI
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // 检查当前参数的类型是否为复杂类型，且不是字符串（你可以根据需求调整这一条件）
            if (context.Metadata.ModelType.IsClass && context.Metadata.ModelType != typeof(string))
            {
                return new BodyModelBinder();
            }

            // 如果不符合条件，则返回 null，系统将使用默认的模型绑定器
            return null;
        }
    }
}
