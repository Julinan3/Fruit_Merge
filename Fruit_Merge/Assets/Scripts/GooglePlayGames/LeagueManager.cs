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
        else if (rank > 50 && rank <= 100) return League.Platinum;
        else if (rank > 100 && rank <= 150) return League.Gold;
        else if (rank > 150 && rank <= 200) return League.Silver;
        else if (rank > 200 && rank <= 210) return League.Bronze;
        else return League.Bronze; // 211 ve sonrası da Bronze
    }
}
