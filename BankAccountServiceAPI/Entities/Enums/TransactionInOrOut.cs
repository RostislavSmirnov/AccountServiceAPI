namespace BankAccountServiceAPI.Entities.Enums
{
    /// <summary>
    /// Перечисление возможных тип транзакций, входящая, или исхдящая.
    /// </summary>
    public enum TransactionInOrOut
    {
        /// <summary>
        /// Исходящяя транзакция
        /// </summary>
        Outgoing,

        /// <summary>
        /// Входящая транзакция
        /// </summary>
        Incoming
    }
}
