using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using SuperMemo.Models;

namespace SuperMemo.Filters
{
    class ErrorHandlerAttribute : ExceptionFilterAttribute
    {
        private const string DefaultErrorMessage = "Technical Error";

        public override void OnException(HttpActionExecutedContext context)
        {
            var message = context.Exception.Message;
            if (string.IsNullOrEmpty(message))
                message = DefaultErrorMessage;
            var error = ResponseObject.Failure(message);
            context.Response = context.Request.CreateResponse(HttpStatusCode.OK, error);
        }
    }
}