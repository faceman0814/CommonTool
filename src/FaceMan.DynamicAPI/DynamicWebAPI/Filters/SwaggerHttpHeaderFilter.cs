using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;
using System.Linq;

namespace FaceMan.DynamicWebAPI.Filters
{
    public class SwaggerHttpHeaderFilter : IOperationFilter
    {


        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            ////var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
            ////var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 

            //如果没有忽略特性
            if (!context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(it => it is IAllowAnonymous))
            {
                OpenApiSchema schema = new OpenApiSchema();

                operation.Parameters.Add(new OpenApiParameter { Name = "LoginToken", In = ParameterLocation.Header, Schema = new OpenApiSchema { Type = "string" }, Description = "登录Token", Required = false });
            }

            ////客户端的api增加
            //if (context.ApiDescription.RelativePath.StartsWith(ConstKeys.ApiPrefixClient))
            //{
            //    //客户端标识
            //    operation.Parameters.Add(new OpenApiParameter { Name = ConstKeys.HeaderClientKey, In = ParameterLocation.Header, Description = "客户端标识", Required = false });
            //}
        }
    }
}
