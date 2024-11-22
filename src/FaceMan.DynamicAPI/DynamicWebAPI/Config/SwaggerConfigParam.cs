using Swashbuckle.AspNetCore.SwaggerUI;

using System.Collections.Generic;

namespace FaceMan.DynamicWebAPI.Config
{
    public class SwaggerConfigParam
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 应用静态文件路径，例如：wwwroot
        /// </summary>
        public string WebRootPath { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系人URL 
        /// </summary>
        public string ContactUrl { get; set; }
        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// API请求类型配置
        /// </summary>
        public List<HttpMethodConfigure> HttpMethods { get; set; }
        /// <summary>
        /// 是否开启文档注释
        /// </summary>
        public bool EnableXmlComments { get; set; }
        /// <summary>
        /// 注释文档路径，生成注释文档时，将XML文件放在该路径下。
        /// </summary>
        public string ApiDocsPath { get; set; }
        /// <summary>
        /// 是否开启API结果过滤器,默认开启
        /// </summary>
        public bool EnableApiResultFilter { get; set; } = true;
        /// <summary>
        /// 是否开启登录页
        /// </summary>
        public bool EnableLoginPage { get; set; }
        /// <summary>
        /// 是否启用深链接，启用后，用户可以直接通过URL访问特定的API操作或模型，而不需要手动导航到相应的位置。
        /// </summary>
        public bool EnableDeepLinking { get; set; }
        /// <summary>
        /// 登录页路径,默认值: /pages/swagger.html
        /// </summary>
        public string LoginPagePath { get; set; } = "/pages/swagger.html";
        /// <summary>
        /// 文档展开方式,none为折叠，list为列表，默认为折叠
        /// </summary>
        public DocExpansion DocExpansion { get; set; } = DocExpansion.None;
        /// <summary>
        /// 配置路由前缀，RoutePrefix是Swagger UI的根路径,默认值: api。
        /// </summary>
        public string RoutePrefix { get; set; } = "api";
        /// <summary>
        /// 设置默认模型展开深度。默认值为3，可以设置成-1以完全展开所有模型。
        /// </summary>
        public int DefaultModelExpandDepth { get; set; } = 3;
        /// <summary>
        /// 配置Endpoint路径
        /// </summary>
        public string SwaggerEndpoint { get; set; } = "/swagger/v1/swagger.json";
        /// <summary>
        /// 时间格式化响应,默认值: yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string DatetimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 公共后缀，用于生成API时会删除。
        /// </summary>
        public string[] CommonPostfixes { get; set; } 
    }
}