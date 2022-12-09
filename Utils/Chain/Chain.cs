namespace PirateQuester.Utils.Chain
{
    public class Chain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RPC { get; set; }
        public Chains Chains { get; set; }
        public bool Enabled { get; set; }
        public string Identifier { get; set; }
        public string HeroAddress { get; set; }
        public string QuestAddress { get; set; }
        public string MeditationAddress { get; set; }
    }
}