using Serilog;
using System.Net;
using System.Text;
using ZstdSharp.Unsafe;

namespace MobvenSozluk.API.Middlewares
{
    public class ElasticLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ElasticLoggingMiddleware> _logger;

        public ElasticLoggingMiddleware(RequestDelegate next, ILogger<ElasticLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            _logger.LogInformation("Request: HTTP {Method} {Path}",
                request.Method, request.Path);


            var originalBodyStream = context.Response.Body;
            MemoryStream responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unhandled exception occurred while processing the request.");
                throw;
            }
            finally
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);

                if (context.Response.StatusCode >= (int)HttpStatusCode.OK && context.Response.StatusCode < (int)HttpStatusCode.Ambiguous)
                {
                    _logger.LogInformation("Response: HTTP {Method} {Path} {StatusCode} \nResponse Body: {ResponseBody}",
                        request.Method, request.Path, context.Response.StatusCode, responseBodyText);               
                }
                else
                {
                    _logger.LogError("Exception: HTTP {Method} {Path} {StatusCode} \nResponse Body: {ResponseBody}",
                    request.Method, request.Path, context.Response.StatusCode, responseBodyText);
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
