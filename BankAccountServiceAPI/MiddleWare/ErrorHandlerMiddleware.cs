using BankAccountServiceAPI.Common;
using System.Net;
using System.Text.Json;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.MiddleWare
{
    /// <summary>
    /// Обработчик ошибок
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        // ReSharper disable once ConvertToPrimaryConstructor считаю обычгый клгструктор более читаемым
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
