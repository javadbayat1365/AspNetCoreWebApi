using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public ApiResultStatusCode statusCode { get; set; }
        public HttpStatusCode httpStatusCode { get; set; }
        public object? AdditionalData{ get; set; }
        public AppException():
            this(ApiResultStatusCode.ServerError)
        {

        }
        public AppException(ApiResultStatusCode statusCode)
            :this(null,statusCode)
        {

        }
        public AppException(string? message)
            :this(message,ApiResultStatusCode.ServerError)
        {
            this.statusCode = ApiResultStatusCode.ServerError;
        }
        public AppException(HttpStatusCode httpStatusCode)
          : this(ApiResultStatusCode.ServerError,null, httpStatusCode)
        {

        }
        public AppException(string? message,ApiResultStatusCode statusCode)
            : this(statusCode,message, HttpStatusCode.InternalServerError)
        {
            this.statusCode = statusCode;
        }
        
        public AppException(ApiResultStatusCode statusCode, string? message, HttpStatusCode httpStatusCode)
           : this(statusCode, message, httpStatusCode, null)
        {

        }
        public AppException(ApiResultStatusCode statusCode, string? message, HttpStatusCode httpStatusCode,Exception exception)
           : this(statusCode, message, httpStatusCode, exception, null)
        {

        }
        public AppException(ApiResultStatusCode statusCode, string? message, HttpStatusCode httpStatusCode, object? AdditionalData)
            :this(statusCode,message,httpStatusCode,null,AdditionalData)
        {

        }
        public AppException(ApiResultStatusCode statusCode,string? message,HttpStatusCode httpStatusCode,Exception exception,object? AdditionalData)
            :base(message,exception)
        {
            this.statusCode = statusCode;
            this.httpStatusCode = httpStatusCode;
            this.AdditionalData = AdditionalData;
        }
    }
}
