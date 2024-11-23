using Microsoft.AspNetCore.Mvc.ModelBinding;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Threading.Tasks;

namespace FaceMan.DynamicWebAPI
{
    public class BodyModelBinder : IModelBinder
    {
        public BodyModelBinder()
        {
        }
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // 只有当类型是复杂类型时才从body绑定
            if (bindingContext.ModelType.IsClass && bindingContext.ModelType != typeof(string))
            {
                // 读取请求体中的内容
                string valueFromBody = string.Empty;
                using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
                {
                    valueFromBody = await reader.ReadToEndAsync();
                }

                try
                {
                    // 反序列化JSON到目标对象
                    var model = JsonConvert.DeserializeObject(valueFromBody, bindingContext.ModelType);
                    bindingContext.Result = ModelBindingResult.Success(model);
                }
                catch (JsonException)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }
            else
            {
                // 不处理非复杂类型
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}
