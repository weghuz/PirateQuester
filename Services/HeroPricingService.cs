using PirateQuester.Utils;

namespace PirateQuester.Services
{
    public class HeroPricingService
    {
        public AccountManager Acc { get; }
        
        public HeroPricingService(AccountManager acc)
        {
            Acc = acc;
        }

    }
}
