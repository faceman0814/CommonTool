using System;

namespace FaceMan.DynamicWebAPI
{
    /// <summary>
    /// 动态WebAPI特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class DynamicWebApiAttribute : Attribute
    {
    }
}
