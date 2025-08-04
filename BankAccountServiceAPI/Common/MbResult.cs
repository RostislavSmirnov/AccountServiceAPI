namespace BankAccountServiceAPI.Common
{
    public class MbResult
    {
        public bool IsSuccess { get; }

        public IEnumerable<MbError> Errors { get; }

        protected MbResult()
        {
            IsSuccess = true;
            Errors = Enumerable.Empty<MbError>();
        }

        protected MbResult(IEnumerable<MbError> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        //Успешный результат
        public static MbResult Success() => new MbResult();

        public static MbResult Failure(IEnumerable<MbError> errors) => new MbResult(errors);

        public static MbResult Failure(MbError error) => new MbResult(new[] { error });
    }


    public class MbResult<T> : MbResult
    {
        public bool IsSuccess { get; } //Успешность операции, если "true" - ошибок нет

        public T Value { get; } //Данные возвращаемые в случае успеха

        public IEnumerable<MbError> Errors { get; } //Список ошибок в случае неудачи

        private MbResult(T value) //Конструктор для успешного результата 
        {
            IsSuccess = true;
            Value = value;
            Errors = Enumerable.Empty<MbError>();
        }
        private MbResult(IEnumerable<MbError> errors) //Конструктор для результата с ошибками
        {
            IsSuccess = false;
            Value = default;
            Errors = errors;
        }

        //Статический метод для создания успешного результата
        public static MbResult<T> Success(T Value) => new MbResult<T>(Value);

        //Статический метод для создания результата с ошибкой
        public static MbResult<T> Failure(IEnumerable<MbError> errors) => new MbResult<T>(errors);

        //Вспомогательный статический метод для создания результата с одной ошибкой
        public static MbResult<T> Failure(MbError error) => new MbResult<T>(new[] { error });
    }

}
