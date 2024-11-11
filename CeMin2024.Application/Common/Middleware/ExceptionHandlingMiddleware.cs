using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using CeMin2024.Application.Exceptions;
using System.Text;

namespace CeMin2024.Application.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no manejado");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResponse
            {
                TraceId = context.TraceIdentifier
            };

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Error de validación";
                    response.Errors = validationException.Errors;
                    break;

                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = notFoundException.Message;
                    break;

                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Message = unauthorizedException.Message;
                    break;

                case ForbiddenAccessException forbiddenException:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    response.Message = forbiddenException.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Se ha producido un error interno del servidor.";
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonResponse = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(jsonResponse, Encoding.UTF8);
        }
    }

    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public string TraceId { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}