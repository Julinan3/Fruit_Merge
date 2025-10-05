using UnityEngine;

public class LeagueListManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI PlayerNameText;
    public TMPro.TextMeshProUGUI LeagueAllScoreText;
    public TMPro.TextMeshProUGUI LeagueNumberText;
    public GameObject[] RankSprites;

    public void SetRank(int rank)
    {
        if (rank >= 1 && rank <= 3)
        {
            foreach (var sprite in RankSprites)
                sprite.SetActive(false);
            RankSprites[rank - 1].SetActive(true);
            LeagueNumberText.gameObject.SetActive(false);
        }
        else
        {
            foreach (var sprite in RankSprites)
                sprite.SetActive(false);
            LeagueNumberText.gameObject.SetActive(true);
            LeagueNumberText.text = rank.ToString();
        }
    }
    public void SetPlayerName(string name)
    {
        PlayerNameText.text = name;
    }
    public void SetLeagueAllScore(long score)
    {
        LeagueAllScoreText.text = score.ToString("N0");
    }

}
