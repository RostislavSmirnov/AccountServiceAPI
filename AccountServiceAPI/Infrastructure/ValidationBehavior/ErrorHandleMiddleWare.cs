using AccountServiceAPI.Infrastructure.ValidationBehavior.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountServiceAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
                // Логируем ЛЮБУЮ ошибку
                _logger.LogError(ex, "Произошла необработанная ошибка: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError; // По умолчанию 500
            object responseBody = new { message = "Произошла внутренняя ошибка сервера.", details = exception.Message };

            
            switch (exception)
            {
                case AppValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    responseBody = validationException.Errors;
                    break;

                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    responseBody = new { message = badRequestException.Message };
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    responseBody = new { message = notFoundException.Message };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
        }
    }
}