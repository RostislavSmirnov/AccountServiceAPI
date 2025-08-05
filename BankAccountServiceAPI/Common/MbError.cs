namespace BankAccountServiceAPI.Common
{
    /// <summary>
    /// Класс описание общего класса для ошибок
    /// </summary>
    public class MbError
    {
        /// <summary>
        /// код ошибки
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global Содержи код ошибки
        public string Code { get; }

        /// <summary>
        /// Описание
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global Содержит описание ошибки
        public string Description { get; }

        /// <summary>
        /// Общий класс ошибок
        /// </summary>
        public MbError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
