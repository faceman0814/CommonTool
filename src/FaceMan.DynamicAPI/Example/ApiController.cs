using FaceMan;

using Microsoft.AspNetCore.Mvc;

namespace FaceMan.Example
{
    /// <summary>
    /// 控制器生成的API接口，不影响原有功能
    /// </summary>
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    public class ApiController 
    {
        [HttpGet]
        public string GetData(string inputParam, string sss)
        {
            return "Hello World!";
        }

        [HttpPost]
        public string CreateData(InputParam input)
        {
            return "Data created with input: " + input.Input;
        }

        [HttpPut]
        public string UPDATEData(InputParam input)
        {
            return "Data UPDATE with input: " + input.Input;
        }
        [HttpDelete]
        public string DeleteData(InputParam input)
        {
            return "Data Delete with input: " + input.Input;
        }
    }
}
