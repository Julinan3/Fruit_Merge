using UnityEngine;

public class LeaguePanel : MonoBehaviour
{
    public GameObject LeagueListPrefab;
    public Transform ListParent; // VerticalLayoutGroup veya benzeri bir parent objesi
    public LeaderboardManager leaderboardManager;

    void Start()
    {
        ShowMyLeagueTop10();
    }

    public void ShowMyLeagueTop10()
    {
        leaderboardManager.GetTop10PerLeague((leagueDict) =>
        {
            if (leagueDict == null) return;

            // Önce eski satýrlarý temizle
            foreach (Transform child in ListParent)
                Destroy(child.gameObject);

            // Oyuncunun ligini bul
            League myLeague = League.Bronze;
            foreach (var kvp in leagueDict)
            {
                foreach (var entry in kvp.Value)
                {
                    if (entry.IsCurrentPlayer)
                    {
                        myLeague = entry.League;
                        break;
                    }
                }
            }

            // O ligin ilk 10'unu göster
            if (leagueDict.TryGetValue(myLeague, out var myLeagueList))
            {
                for (int i = 0; i < myLeagueList.Count; i++)
                {
                    var entry = myLeagueList[i];
                    int bronzeRank = i + 1; // Bronze içindeki sýrasý

                    var go = Instantiate(LeagueListPrefab, ListParent);
                    var manager = go.GetComponent<LeagueListManager>();
                    manager.SetRank(entry.Rank);
                    manager.SetPlayerName(entry.PlayerName);
                    manager.SetLeagueAllScore(entry.Score);
                }
            }
        });
    }
}
