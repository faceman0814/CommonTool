using FaceMan.DynamicWebAPI.Dependencys;
using FaceMan.DynamicWebAPI.ErrorExceptions;

using System;
namespace FaceMan.DynamicWebAPI
{
    public abstract class ApplicationService : IApplicationService, ITransientDependency
    {
        /// <summary>
        /// API 通用后缀
        /// </summary>
        public static string[] CommonPostfixes { get; set; } = { "AppService", "ApplicationService", "Service" };

        protected virtual void ThrowUserFriendlyError(string reason)
        {
            throw new UserFriendlyException("Error:" + reason + "操作于：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
