using Static.Enums;

namespace Services.Currency
{
    public interface ICurrencyService
    {
        public void AddCurrency(ECurrencyType type, int amount);
    }
}