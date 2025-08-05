namespace BankAccountServiceAPI.Entities.Enums
{
    /// <summary>
    /// Вид транзакции, кредитная, или дебетовая
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Кредитная транзакция
        /// </summary>
        Credit,

        /// <summary>
        /// Обычная транзакция
        /// </summary>
        Debit
    }
}
