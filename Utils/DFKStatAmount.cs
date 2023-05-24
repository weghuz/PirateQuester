namespace PirateQuester.Utils
{
    public class DFKStatAmount : DFKStat
    {
        public DFKStatAmount(byte id, int amount) : base(id, Constants.DFKStats[id].Name)
        {
            Amount = amount;
        }
        public int Amount { get; set; }
    }
}
