using MobvenSozluk.Domain.Constants;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using System.Net;
using System.Text.Json;

namespace MobvenSozluk.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;
            var exceptionType = exception.GetType();

            switch (exceptionType.Name)
            {
                case MagicStrings.BadRequestExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.BadRequest;
                    break;
                case MagicStrings.NotFoundExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;
                case MagicStrings.ArgumentNullExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;
                case MagicStrings.NotImplementedExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.NotImplemented;
                    break;
                case MagicStrings.KeyNotFoundExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.Unauthorized;
                    break;
                case MagicStrings.ConflictExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.Conflict;
                    break;
                case MagicStrings.ForbiddenExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.Forbidden;
                    break;
                case MagicStrings.UnauthorizedAccessExceptionValue:
                    message = exception.Message;
                    status = HttpStatusCode.Unauthorized;
                    break;
                default:
                    message = exception.Message;
                    status = HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var result = CustomResponseDto<NoContentDto>.Fail((int)status, message);
            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}