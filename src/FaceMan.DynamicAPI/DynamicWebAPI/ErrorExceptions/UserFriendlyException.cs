using System;
using System.Runtime.Serialization;

namespace FaceMan.DynamicWebAPI.ErrorExceptions
{
    [Serializable]
    public class UserFriendlyException : Exception
    {
        public static LogSeverity DefaultLogSeverity = LogSeverity.Warn;
        /// <summary>
        /// 错误详情
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// 任意错误代码。
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 例外的严重程度。默认值：警告。
        /// </summary>
        public LogSeverity Severity { get; set; }
        public UserFriendlyException()
        {
            Severity = DefaultLogSeverity;
        }

        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }


        public UserFriendlyException(string message)
            : base(message)
        {
            Severity = DefaultLogSeverity;
        }

        public UserFriendlyException(string message, LogSeverity severity)
            : base(message)
        {
            Severity = severity;
        }

        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        public UserFriendlyException(string message, string details)
            : this(message)
        {
            Details = details;
        }

        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = DefaultLogSeverity;
        }

        public UserFriendlyException(string message, string details, Exception innerException)
            : this(message, innerException)
        {
            Details = details;
        }

    }
}
