public enum League
{
    Bronze,
    Silver,
    Gold,
    Platinum,
    Diamond
}

public class LeagueManager
{
    public static League GetLeague(int rank, long score)
    {
        // Diamond: hem ilk 50 hem 15000+ skor
        if (rank > 0 && rank <= 50 && score >= 15000) return League.Diamond;
        else if (rank <= 150) return League.Platinum;
        else if (rank <= 300) return League.Gold;
        else if (rank <= 1000) return League.Silver;
        else return League.Bronze;
    }
}
