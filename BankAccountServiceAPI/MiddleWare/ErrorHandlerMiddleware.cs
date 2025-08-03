using BankAccountServiceAPI.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankAccountServiceAPI.MiddleWare
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
                _logger.LogError(ex, "Произошло необрабатываемое исключение.");

                //Ответ для клиента
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = MbResult.Failure(new MbError("Внутренняя ошибка сервера",
                    "Произошла непредвиденная внутренняя ошибка сервера."));

                var jsonResponse = JsonSerializer.Serialize(errorResponse.Errors);

                await response.WriteAsync(jsonResponse);
            }
        }
    }
}
