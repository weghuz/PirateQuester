namespace DFK;

public class HeroesResponse
{
    public class Data
    {
        public Hero[] heroes { get; set; }
    }
    public Data data { get; set; }
}