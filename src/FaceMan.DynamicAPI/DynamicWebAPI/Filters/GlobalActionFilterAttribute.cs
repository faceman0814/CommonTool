using FaceMan.DynamicWebAPI.ErrorExceptions;
using FaceMan.DynamicWebAPI.Extensions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Linq;

namespace FaceMan.DynamicWebAPI.Filters
{
    /// <summary>
    /// 全局的action过滤器，主要用来验证数据类型
    /// </summary>
    public class GlobalActionFilterAttribute : ActionFilterAttribute
    {
        private ILogger<GlobalActionFilterAttribute> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GlobalActionFilterAttribute(ILogger<GlobalActionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (!HandleException(context))
                {
                    return;
                }
            }
            else
            {
                ProcessResult(context);
            }


            base.OnActionExecuted(context);
        }
        private bool HandleException(ActionExecutedContext context)
        {
            Exception ex = context.Exception;
            if (ex is ApiException)
            {
                context.Result = new JsonResult(ApiResponse.SetError(
                    (ex as ApiException).ErrMsg, (ex as ApiException).ErrCode)
                   );
            }
            else
            {
                context.Result = new JsonResult(ApiResponse.SetError(ex.Message, (int)ApiError.全局错误));
            }
            context.ExceptionHandled = true;
            return true;
        }
        private void ProcessResult(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult result)
            {
                if (result.StatusCode < 200 || result.StatusCode >= 300)
                {
                    var apiResponse = ApiResponse<object>.SetError("错误或无效响应", data: result.Value);
                    SetContentResult(context, apiResponse, result.StatusCode ?? StatusCodes.Status500InternalServerError);
                }
                else
                {
                    //可能有多层嵌套，需要解包。
                    if (context.Result is ObjectResult objectResult)
                    {
                        var apiResponse = UnwrapResponse(objectResult.Value);
                        SetContentResult(context, ApiResponse<object>.SetSuccess(apiResponse), result.StatusCode ?? StatusCodes.Status200OK);
                    }
                }
            }
            else
            {
                //可能是void
                SetContentResult(context, ApiResponse<object>.SetSuccess(null), StatusCodes.Status200OK);
            }
        }
        public object UnwrapResponse(object result)
        {
            while (result != null && IsApiResponse(result))
            {
                var propertyInfo = result.GetType().GetProperty("Data");
                if (propertyInfo != null)
                {
                    result = propertyInfo.GetValue(result);
                }
                else
                {
                    break;
                }
            }
            return result;
        }
        private bool IsApiResponse(object obj)
        {
            // 检查对象是否是ApiResponse<T>的实例
            var type = obj.GetType();
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
        private static void SetContentResult(ActionExecutedContext context, ApiResponse<object> response, int statusCode)
        {
            context.Result = new ContentResult
            {
                StatusCode = statusCode,
                ContentType = "application/json;charset=utf-8",
                Content = JsonConvert.SerializeObject(response)
            };
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //校验参数，校验DTO的System.ComponentModel.DataAnnotations
            if (!context.ModelState.IsValid)
            {
                var errorMsg = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                throw new ApiException(ApiError.参数错误, errorMsg);
            }
        }
    }
}
