namespace PirateQuester.Utils.Chain
{
    public class Chain
    {
        public Chain() 
        {
            
        }
        public Chain(ChainDTO dto)
        {
            enabled = dto.Enabled;
            rpc = dto.RPC;
        }
        public int Id { get; set; }
        public string Name { get; set; }
		private string rpc;
		public string RPC { get { return rpc; } set { rpc = value; Save(); } }
        public ChainEnum ChainEnum { get; set; }
        private bool enabled;
        public bool Enabled { get { return enabled; } set { enabled = value; Save(); } }
        public string Identifier { get; set; }
        public string HeroAddress { get; set; }
        public string QuestAddress { get; set; }
        public string MeditationAddress { get; set; }
        public string QuestRewarder { get; set; }
		public AccountSettings SettingsManager { get; internal set; }
		public void Save()
		{
			if (SettingsManager is null)
                return;
            SettingsManager.SaveChainSettings();
		}
	}
}