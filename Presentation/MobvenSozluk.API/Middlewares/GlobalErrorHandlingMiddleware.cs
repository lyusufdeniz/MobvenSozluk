using MobvenSozluk.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = MobvenSozluk.Infrastructure.Exceptions.NotFoundException;
using NotImplementedException = MobvenSozluk.Infrastructure.Exceptions.NotImplementedException;
using UnauthorizedAccessException = MobvenSozluk.Infrastructure.Exceptions.UnauthorizedAccessException;
using BadRequestException = MobvenSozluk.Infrastructure.Exceptions.BadRequestException;
using NotFoundException = MobvenSozluk.Infrastructure.Exceptions.NotFoundException;
using ConflictException = MobvenSozluk.Infrastructure.Exceptions.ConflictException;
using MobvenSozluk.API.Controllers;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MongoDB.Bson;

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
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            HttpStatusCode status;
            string message;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = exception.Message;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exceptionType == typeof(ConflictException))//409
            {
                status = HttpStatusCode.Conflict;
                message = exception.Message;
            }
            else if (exceptionType == typeof(ForbiddenException))
            {
                status = HttpStatusCode.Forbidden;
                message = exception.Message;
            }
            else if (exceptionType == typeof(EntityException))
            {
                status = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else//500
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
            }
            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message,
                statatusCode = status,
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;


            var result = CustomResponseDto<NoContentDto>.Fail((int)status, message);
            return context.Response.WriteAsync(result.ToJson());
        }
    }
}