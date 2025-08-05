using BankAccountServiceAPI.Common;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 

namespace BankAccountServiceAPI.Features
{
    /// <summary>
    /// Контроллер с универсальными методами обработки 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ActionResult HandleResult<T>(MbResult<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            //Здесь можно добавить логику для разных кодов ошибок
            //Например, если result.Errors содержит ошибку с кодом "NotFound", возвращать NotFound().
            return BadRequest(result.Errors);
        }

        protected ActionResult HandleResult(MbResult result)
        {
            if (result.IsSuccess)
            {
                return NoContent(); //Для успешных операций без возврата данных (например, DELETE)
            }
            return BadRequest(result.Errors);
        }
    }
}
