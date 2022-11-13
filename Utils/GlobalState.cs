using DFK;

namespace Utils;

public class GlobalState
{
    public List<string> LoadedOwnersHomescreen { get; set; } = new();
    public List<Hero> LoadedHeroesHomeScreen { get; set; } = new();
}