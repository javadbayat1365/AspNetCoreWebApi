using Common.Enums;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode statusCode { get; set; }
        public string? Message { get; set; }
        public ApiResult()
        {

        }
        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string? message=null)
        {
            IsSuccess = isSuccess;
            this.statusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }
        #region Implicit Operators

            public static implicit operator ApiResult(OkResult value)
            => new ApiResult { IsSuccess = true,statusCode = ApiResultStatusCode.Success};
        
        public static implicit operator ApiResult(NotFoundObjectResult notFoundObjectResult)
        {
            var value = "";
            if (notFoundObjectResult.Value is SerializableError errors)
            {
                var error = errors.SelectMany(p => (string[])p.Value).Distinct();
                value = string.Join(" | ",error);
            }
         return   new ApiResult { IsSuccess = false, statusCode = ApiResultStatusCode.NotFound, Message = value };
        }

        public static implicit operator ApiResult(NotFoundResult notFoundResult)
            => new ApiResult { IsSuccess = false, statusCode = ApiResultStatusCode.NotFound };

        public static implicit operator ApiResult(BadRequestResult badRequestResult)
            => new ApiResult { IsSuccess = false, statusCode = ApiResultStatusCode.BadRequest, Message="خطا در پارامترهای ورودی"};
            
        public static implicit operator ApiResult(BadRequestObjectResult badRequestObjectResult)
        {
            var Errors = badRequestObjectResult.Value.ToString();
            if (badRequestObjectResult.Value is SerializableError errors) 
            {
                var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
                Errors = string.Join(" | ",errorMessage);
            }
           return new ApiResult(
            
                message: Errors,
                isSuccess : false,
                statusCode: ApiResultStatusCode.BadRequest
            );
        }

        public static implicit operator ApiResult(ContentResult contentResult)
            =>new ApiResult(isSuccess : true, statusCode: ApiResultStatusCode.Success,message: contentResult.Content);
            
        #endregion
    }
    public class ApiResult<TData>:ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData?  Data { get; set; }
        public ApiResult()
        {

        }
        public ApiResult(TData? data,ApiResultStatusCode statusCode, bool isSuccess, string? message=null)
            :base(isSuccess, statusCode, message)
        {
            Data = data;
        }

        public static implicit operator ApiResult<TData>(TData value)
        {
            return new ApiResult<TData>(
                data : value,
                isSuccess : true,
                message : "",
                statusCode : ApiResultStatusCode.Success
            );
        }

        public static implicit operator ApiResult<TData>(OkObjectResult value)
            => new ApiResult<TData>()
        {
            IsSuccess = true,
            Message = null,
            statusCode = ApiResultStatusCode.Success,
             Data  = (TData)value.Value
        };

        public static implicit operator ApiResult<TData>(OkResult value) 
            => new ApiResult<TData>(
            data:null,
            isSuccess: true,
            message : null,
            statusCode : ApiResultStatusCode.Success
        );

        public static implicit operator ApiResult<TData>(ContentResult value)
        =>  new ApiResult<TData> { IsSuccess =true, statusCode = ApiResultStatusCode.Success, Data = null, Message= value.Content };

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult<TData>(null, ApiResultStatusCode.BadRequest,true, message);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(null, ApiResultStatusCode.NotFound, false);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>((TData)result.Value, ApiResultStatusCode.NotFound, false);
        }
    }
}
