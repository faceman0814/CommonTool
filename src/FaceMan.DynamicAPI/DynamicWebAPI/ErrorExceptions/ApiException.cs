using System;
using System.ComponentModel;
using System.Reflection;

namespace FaceMan.DynamicWebAPI.ErrorExceptions
{
    public class ApiException : Exception
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public ApiException(ApiError error, string errorMsg = null)
        {
            ErrCode = (int)error;
            ErrMsg = string.IsNullOrWhiteSpace(errorMsg) ? GetDescription(error) : errorMsg;
        }

        public string GetDescription(Enum obj)
        {
            string val = string.Empty;
            try
            {
                val = obj.ToString();
                Type t = obj.GetType();
                FieldInfo fi = t.GetField(val);
                DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (arrDesc != null && arrDesc.Length > 0)
                {
                    val = arrDesc[0].Description;
                }

            }
            catch
            {

            }

            return val;

        }
    }
}
