using FaceMan.DynamicWebAPI;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace FaceMan.Example
{
    /// <summary>
    /// 继承IApplicationService实现的动态API服务
    /// </summary>
    public class TestAppService : IApplicationService
    {
        /// <summary>
        /// 实现GetDataAPI接口
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetData(InputParam inputParam)
        {
            return await Task.FromResult("Hello World!");
        }

        /// <summary>
        /// 实现CreateDataAPI接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CreateData(InputParam input)
        {
            return "Data created with input: " + input.Input;
        }

        public string UPDATEData(InputParam input)
        {
            return "Data UPDATE with input: " + input.Input;
        }

        public string DeleteData(InputParam input)
        {
            return "Data Delete with input: " + input.Input;
        }
    }

    public class InputParam
    {
        public string Input { get; set; }
        public string Input1 { get; set; }

    }
}
