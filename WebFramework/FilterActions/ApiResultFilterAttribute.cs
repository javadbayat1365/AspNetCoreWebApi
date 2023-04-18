using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Api;

namespace WebFramework.FilterActions
{
    public class ApiResultFilterAttribute:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResultValue)
            {
                context.Result = new JsonResult(new ApiResult<object>(context.Result, Common.Enums.ApiResultStatusCode.IsSuccess,true));
            }
            base.OnResultExecuting(context);
        }
    }
}
