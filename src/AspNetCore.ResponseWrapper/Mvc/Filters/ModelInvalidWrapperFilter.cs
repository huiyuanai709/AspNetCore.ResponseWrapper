using System;
using System.Linq;
using AspNetCore.ResponseWrapper.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ResponseWrapper.Mvc.Filters
{
    public class ModelInvalidWrapperFilter : IActionFilter
    {
        private readonly IResponseWrapper _responseWrapper;
        private readonly ILogger<ModelInvalidWrapperFilter> _logger;

        public ModelInvalidWrapperFilter(IResponseWrapper responseWrapper, ILoggerFactory loggerFactory)
        {
            _responseWrapper = responseWrapper;
            _logger = loggerFactory.CreateLogger<ModelInvalidWrapperFilter>();
        }

        private static readonly Action<ILogger, Exception?> ModelStateInvalidFilterExecuting = LoggerMessage.Define(
            LogLevel.Debug,
            new EventId(1, "ModelStateInvalidFilterExecuting"),
            "The request has model state errors, returning an error response.");
    
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Result == null && !context.ModelState.IsValid)
            {
                ModelStateInvalidFilterExecuting(_logger, null);
                context.Result = new ObjectResult(_responseWrapper.ClientError(string.Join(",",
                    context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}