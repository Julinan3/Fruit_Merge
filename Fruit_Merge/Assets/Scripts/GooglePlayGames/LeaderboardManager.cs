using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard ID")]
    public string LEADERBOARD_ID = "REPLACE_WITH_LEADERBOARD_ID";

    public void PostScore(long score)
    {
        if (!GPGSManager.Instance.IsAuthenticated())
        {
            Debug.LogWarning("Not authenticated, retrying login...");
            GPGSManager.Instance.LoginGooglePlayGames();
            return;
        }

        PlayGamesPlatform.Instance.ReportScore(score, LEADERBOARD_ID, success =>
        {
            Debug.Log($"PostScore result: {success} score:{score}");
        });
    }

    public void ShowLeaderboardUI()
    {
        if (!GPGSManager.Instance.IsAuthenticated())
        {
            Debug.LogWarning("Not authenticated, retrying login...");
            GPGSManager.Instance.LoginGooglePlayGames();
            return;
        }

        PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD_ID);
    }

    public void LoadPlayerScore(Action<int, long> callback)
    {
        if (!GPGSManager.Instance.IsAuthenticated())
        {
            callback?.Invoke(-1, 0);
            return;
        }

        PlayGamesPlatform.Instance.LoadScores(
            LEADERBOARD_ID,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                if (data.Valid && data.PlayerScore != null)
                {
                    int rank = data.PlayerScore.rank;
                    long score = data.PlayerScore.value;
                    callback?.Invoke(rank, score);
                }
                else
                {
                    callback?.Invoke(-1, 0);
                }
            });
    }
}
