namespace BankAccountServiceAPI.Common
{
    public class MbError
    {
        //Код ошибки
        public string Code { get; }

        //Описание ошибки
        public string Description { get; }

        public MbError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
