namespace PirateQuester.Bot
{
    public class QuestEnabled
    {
        public int QuestId { get; set; }
        public bool Enabled { get; set; }

        private bool questEagerly;
        public bool QuestEagerly { get => questEagerly; set { if (value) { questInstantly = false; } questEagerly = value; } }
        private bool questInstantly;
        public bool QuestInstantly { get { return questInstantly; } set { if (value) { QuestEagerly = false; } questInstantly = value; } }
        public bool CapAttempts { get; set; }
        public int? MinStamina { get; set; }
    }
}