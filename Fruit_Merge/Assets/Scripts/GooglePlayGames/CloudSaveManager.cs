using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    public void SaveData(string saveName, string jsonData)
    {
        if (!Social.localUser.authenticated)
        {
            Debug.LogWarning("Google Play Games ile giriþ yapýlmamýþ, save iþlemi yapýlamaz.");
            return;
        }

        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
            saveName,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    byte[] data = Encoding.UTF8.GetBytes(jsonData);
                    SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder()
                        .WithUpdatedPlayedTime(game.TotalTimePlayed)
                        .Build();
                    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, data, (s, g) =>
                    {
                        if (s == SavedGameRequestStatus.Success) Debug.Log("Cloud save baþarýlý!");
                        else Debug.LogWarning("Cloud save baþarýsýz!");
                    });
                }
            });
    }

    public void LoadData(string saveName, Action<string> callback)
    {
        if (!Social.localUser.authenticated)
        {
            callback?.Invoke(null);
            return;
        }

        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
            saveName,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, (s, data) =>
                    {
                        if (s == SavedGameRequestStatus.Success)
                        {
                            string json = Encoding.UTF8.GetString(data);
                            callback?.Invoke(json);
                        }
                        else callback?.Invoke(null);
                    });
                }
                else callback?.Invoke(null);
            });
    }
}
