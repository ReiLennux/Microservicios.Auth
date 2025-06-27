using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using ILogger = NLog.ILogger;
namespace KnowCloud.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            logger.Error(context.Exception, "An unhandled exception occurred.");

            context.Result = new ObjectResult(new
            {
                Message = "An error occurred while processing your request.",
                Details = context.Exception.Message
            })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}
