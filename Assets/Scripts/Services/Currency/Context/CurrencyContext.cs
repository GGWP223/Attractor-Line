using Static.Enums;

namespace Services.Currency.Context
{
    public class CurrencyContext
    {
        public ECurrencyType CurrencyType { get; }
        public int Amount { get; set; }

        public CurrencyContext(ECurrencyType currencyType)
        {
            CurrencyType = currencyType;
        }
    }
}