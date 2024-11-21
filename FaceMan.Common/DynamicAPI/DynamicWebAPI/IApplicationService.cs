namespace DynamicAPI.DynamicWebAPI
{
    /// <summary>
    /// 动态WebAPI接口
    /// </summary>
    public interface IApplicationService
    {
        TService GetService<TService>();
    }
}
