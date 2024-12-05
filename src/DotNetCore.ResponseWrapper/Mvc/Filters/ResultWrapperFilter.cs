using DotNetCore.ResponseWrapper.Abstractions;
using DotNetCore.ResponseWrapper.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetCore.ResponseWrapper.Mvc.Filters
{
    public class ResultWrapperFilter : IResultWrapperFilter
    {
        private readonly IResponseWrapper _responseWrapper;
        private readonly IResponseWrapper<object?> _responseWithDataWrapper;

        public ResultWrapperFilter(IResponseWrapper responseWrapper, IResponseWrapper<object?> responseWithDataWrapper)
        {
            _responseWrapper = responseWrapper;
            _responseWithDataWrapper = responseWithDataWrapper;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Result)
            {
                case EmptyResult:
                case OkResult:
                    context.Result = new OkObjectResult(_responseWrapper.Ok());
                    return;
                case ObjectResult objectResult:
                    context.Result = new OkObjectResult(_responseWithDataWrapper.Ok(objectResult.Value));
                    return;
                case JsonResult jsonResult:
                    context.Result = new OkObjectResult(_responseWithDataWrapper.Ok(jsonResult.Value));
                    return;
            }
        }
    }
}