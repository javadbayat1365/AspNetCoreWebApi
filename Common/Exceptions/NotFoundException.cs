using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            :base(ApiResultStatusCode.NotFound, null, System.Net.HttpStatusCode.NotFound)
        {

        }
        public NotFoundException(string? message)
            : base(message, ApiResultStatusCode.NotFound)
        {
        }
        public NotFoundException(string? message,object? additionalData)
            :base(ApiResultStatusCode.NotFound,message,System.Net.HttpStatusCode.NotFound,additionalData)
        {

        }
        public NotFoundException(string? message,object? additionalData,Exception exception)
            :base(ApiResultStatusCode.NotFound,message,System.Net.HttpStatusCode.NotFound,exception,additionalData)
        {

        }
    }
}
