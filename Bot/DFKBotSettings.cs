using DFK;

namespace PirateQuester.Bot
{
    public class DFKBotSettings
    {
        public int UpdateInterval { get; set; } = 180;
        public int MinStamina { get; set; } = 20;
        public int MaxGasFeeGwei { get; set; } = 200;
    }
}