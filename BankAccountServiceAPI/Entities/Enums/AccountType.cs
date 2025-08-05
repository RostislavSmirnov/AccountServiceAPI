namespace BankAccountServiceAPI.Entities.Enums
{
    /// <summary>
    /// Перечисления возможных типов аккаунта
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Обычный счёт
        /// </summary>
        Checking,

        /// <summary>
        /// Накопительный счёт
        /// </summary>
        Deposit, 

        /// <summary>
        /// Кредитный счёт
        /// </summary>
        Credit   
    }
}
