#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace BankAccountServiceAPI.Common
{
    /// <summary>
    /// Класс для описании результата без возвращаемого значения
    /// </summary>
    public class MbResult
    {
        public bool IsSuccess { get; }

        public IEnumerable<MbError> Errors { get; }

        protected MbResult()
        {
            IsSuccess = true;
            Errors = [];
        }

        protected MbResult(IEnumerable<MbError> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        public static MbResult Success() => new MbResult();

        public static MbResult Failure(IEnumerable<MbError> errors) => new MbResult(errors);

        public static MbResult Failure(MbError error) => new MbResult([error]);
    }


    public class MbResult<T> : MbResult
    {
        public new bool IsSuccess { get; } //Успешность операции, если "true" - ошибок нет

        public T Value { get; } //Данные возвращаемые в случае успеха

        public new IEnumerable<MbError> Errors { get; } //Список ошибок в случае неудачи

        private MbResult(T value) //Конструктор для успешного результата 
        {
            IsSuccess = true;
            Value = value; //Resharper предлогает непонятное решение
            Errors = [];
        }
        private MbResult(IEnumerable<MbError> errors) //Конструктор для результата с ошибками
        {
            IsSuccess = false;
            Value = default;
            Errors = errors;
        }

        //Статический метод для создания успешного результата
        public static MbResult<T> Success(T Value) => new(Value);

        //Статический метод для создания результата с ошибкой
        public new static MbResult<T> Failure(IEnumerable<MbError> errors) => new(errors);

        //Вспомогательный статический метод для создания результата с одной ошибкой
        public new static MbResult<T> Failure(MbError error) => new([error]);
    }

}
