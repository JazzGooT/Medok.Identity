using FluentValidation;
using MedokStore.Application.Common.Exceptions;

using System.Net;
using System.Text.Json;

namespace MedokStore.Identity.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate netx) => _next = netx;
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exeption)
            {
                await HanleExeptionAsync(context, exeption);
            }
        }
        private static Task HanleExeptionAsync(HttpContext context, Exception exeption)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exeption)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exeption.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
