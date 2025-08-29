using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] Fruits;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
