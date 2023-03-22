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
                case "BadRequestException":
                    message = exception.Message;
                    status = HttpStatusCode.BadRequest;
                    break;
                case "NotFoundException":
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;
                case "ArgumentNullException":
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;
                case "NotImplementedException":
                    message = exception.Message;
                    status = HttpStatusCode.NotImplemented;
                    break;
                case "KeyNotFoundException":
                    message = exception.Message;
                    status = HttpStatusCode.Unauthorized;
                    break;
                case "ConflictException":
                    message = exception.Message;
                    status = HttpStatusCode.Conflict;
                    break;
                case "ForbiddenException":
                    message = exception.Message;
                    status = HttpStatusCode.Forbidden;
                    break;
                case "UnauthorizedAccessException":
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