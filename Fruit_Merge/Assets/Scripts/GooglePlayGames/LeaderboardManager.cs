using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard ID")]
    public string LEADERBOARD_ID = "REPLACE_WITH_LEADERBOARD_ID";

    public class LeaderboardEntry
    {
        public int Rank;
        public string PlayerName;
        public long Score;
        public League League;
        public bool IsCurrentPlayer;
    }

    // Her ligin ilk 10 oyuncusunu döndürür
    public void GetTop10PerLeague(Action<Dictionary<League, List<LeaderboardEntry>>> callback)
    {
        if (!GPGSManager.Instance.IsAuthenticated())
        {
            Debug.LogWarning("Not authenticated, retrying login...");
            GPGSManager.Instance.LoginGooglePlayGames();
            callback?.Invoke(null);
            return;
        }

        PlayGamesPlatform.Instance.LoadScores(
            LEADERBOARD_ID,
            LeaderboardStart.TopScores,
            210, // Ýlk 210 oyuncu çekiliyor
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.Weekly, // <-- Haftalýk skorlar
            (data) =>
            {
                var leagueDict = new Dictionary<League, List<LeaderboardEntry>>();
                foreach (League l in Enum.GetValues(typeof(League)))
                    leagueDict[l] = new List<LeaderboardEntry>();

                string myName = Social.localUser.userName;

                if (data.Valid && data.Scores != null)
                {
                    foreach (var score in data.Scores)
                    {
                        var league = LeagueManager.GetLeague(score.rank, score.value);
                        var entry = new LeaderboardEntry
                        {
                            Rank = score.rank,
                            PlayerName = score.userID == Social.localUser.id ? myName : score.userID,
                            Score = score.value,
                            League = league,
                            IsCurrentPlayer = score.userID == Social.localUser.id
                        };
                        // Her lig için ilk 10'u ekle
                        if (leagueDict[league].Count < 10)
                            leagueDict[league].Add(entry);
                    }
                }

                callback?.Invoke(leagueDict);
            });
    }
}
