using FaceMan.DynamicWebAPI.Extensions;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceMan.DynamicWebAPI.Filters
{
    public class RemoveAppFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newTags = new List<OpenApiTag>();
            // 去掉控制器分类中的 "App" 后缀
            foreach (var path in swaggerDoc.Paths.Values)
            {
                foreach (var operation in path.Operations.Values)
                {
                    if (operation.Tags.Any(tag => ApplicationService.CommonPostfixes.Any(postfix => tag.Name.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))))
                    {
                        var tags = operation.Tags.Select(tag => new OpenApiTag
                        {
                            Name = tag.Name.RemovePostFix(ApplicationService.CommonPostfixes.FirstOrDefault(postfix => tag.Name.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))), // 根据需要选择合适的后缀
                            Description = tag.Description
                        }).ToList();

                        operation.Tags.Clear();
                        foreach (var tag in tags)
                        {
                            operation.Tags.Add(tag);
                        }
                    }
                }
            }
        }
    }
}
