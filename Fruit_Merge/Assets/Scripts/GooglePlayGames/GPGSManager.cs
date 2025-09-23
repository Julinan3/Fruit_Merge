using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager Instance { get; private set; }

    public string Token;
    public string Error;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Play Games baþlat
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play Games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                });
            }
            else
            {
                Error = "Failed to retrieve Google Play Games authorization code";
                Debug.LogWarning("Login Unsuccessful, status: " + status);
            }
        });
    }

    public bool IsAuthenticated()
    {
        return Social.localUser != null && Social.localUser.authenticated;
    }
}
