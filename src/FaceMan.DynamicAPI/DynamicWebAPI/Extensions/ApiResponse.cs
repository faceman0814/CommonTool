using System;

namespace FaceMan.DynamicWebAPI.Extensions
{
    public class ApiResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }


        public static ApiResponse SetSuccess(string msg = null)
        {
            return new ApiResponse()
            {
                Code = 0,
                Message = msg,
                Success = true
            };
        }

        public static ApiResponse SetError(string errorMsg, int code = 9999)
        {
            return new ApiResponse()
            {
                Code = code,
                Message = errorMsg,
                Success = false
            };
        }

        public void Throw()
        {
            if (Success == false)
                throw new Exception(Message);
        }

    }

    /// <summary>
    /// 响应数据结构体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T> : ApiResponse
    {

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        public static ApiResponse<T> SetSuccess(T data)
        {
            return new ApiResponse<T>()
            {
                Code = 0,
                Message = null,
                Success = true,
                Data = data
            };
        }

        public static ApiResponse<T> SetResult(ApiResponse response, T data)
        {
            return new ApiResponse<T>()
            {
                Code = response.Code,
                Message = response.Message,
                Success = response.Success,
                Data = data
            };
        }



        public static ApiResponse<T> SetError(string errorMsg, int code = -1, T data = default)
        {
            return new ApiResponse<T>()
            {
                Code = code,
                Message = errorMsg,
                Success = false,
                Data = data
            };

        }

        public T ThrowGet()
        {
            if (Success == false)
                throw new Exception(Message);

            return Data;
        }
    }
}
