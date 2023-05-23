using Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Api;

namespace WebFramework.Middlewares
{
    public static class ExceptionHadler
    {
        public static IApplicationBuilder ExceptionHadlerMiddle(this IApplicationBuilder builder)
        {
           return builder.UseMiddleware<ExceptionHadlerMiddleware>();
        }
    }


    internal class ExceptionHadlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHadlerMiddleware> logger;
        private readonly IHostingEnvironment environment;

        public ExceptionHadlerMiddleware(RequestDelegate next,ILogger<ExceptionHadlerMiddleware> logger, IHostingEnvironment  environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }
        public async Task Invoke(HttpContext httpContext)
        {
                httpContext.Response.ContentType = "Application/json";
                string message = "";
            try
            {
                if (!httpContext.Request.IsHttps)
                {
                    throw new ApplicationException();
                }
                await next(httpContext);
            }
            catch(ApplicationException ex)
            {
                logger.LogError(ex,ex.Message);
                var resultApi = new ApiResult(false, Common.Enums.ApiResultStatusCode.ApplicationError) { };
                var json = JsonConvert.SerializeObject(resultApi);
                await httpContext.Response.WriteAsync(json);
            }
            catch (AppException ex)
            {
                if (environment.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>()
                    {
                        ["Exception"]= ex.Message, 
                        ["StackTrance"]= ex.InnerException.StackTrace,
                    };
                    if (ex.InnerException != null)
                    {
                        dic.Add("InnerException.Exception",ex.InnerException.Message);
                        dic.Add("InnerException.StackTrace",ex.InnerException.StackTrace);
                    }
                    var jsondic = JsonConvert.SerializeObject(dic);
                    message = jsondic;
                }
                else
                {
                    message = ex.Message;
                }
                logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = (int)ex.httpStatusCode;
                var resultApi = new ApiResult(false, ex.statusCode,message) { };
                var json = JsonConvert.SerializeObject(resultApi);
                
                await httpContext.Response.WriteAsync(json);
            }
           
            catch (Exception ex)
            {
                logger.LogError(ex,"خطای سروری رخ داده است");
                var result = new ApiResult(false, Common.Enums.ApiResultStatusCode.ServerError) { };
                var json = JsonConvert.SerializeObject(result);
                await httpContext.Response.WriteAsync(json);
            }
            
        }
    }
}
