namespace PirateQuester.Utils
{
    public class FinishedTransaction
    {
        public bool Success { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public string TransactionHash { get; set; }
    }
}
