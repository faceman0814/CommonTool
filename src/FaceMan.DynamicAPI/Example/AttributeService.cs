using FaceMan.DynamicWebAPI;

namespace FaceMan.Example
{
    /// <summary>
    /// 用DynamicWebApi特性实现的动态API服务
    /// </summary>
    //[DynamicWebApi]
    public class AttributeService
    {
        public string GetData(InputParam inputParam)
        {
            return "Hello World!";
        }

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
}
