using BankAccountServiceAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountServiceAPI.Features
{
    //Контроллер с универсальным методами обработки
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
