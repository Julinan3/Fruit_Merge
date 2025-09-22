using System;
using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int level;
    private AudioSource mergeSound;
    [SerializeField] private GameObject[] mergeEffects;
    [HideInInspector]public bool isMerging = false;

    private void Awake()
    {
        mergeSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMerging) return;

        Fruit other = collision.gameObject.GetComponent<Fruit>();
        if (other != null && other.level == level)
        {
            StartCoroutine(Merge(other));
        }
    }

    IEnumerator Merge(Fruit other)
    {
        isMerging = true;
        other.isMerging = true;

        Vector3 mergePos = (transform.position + other.transform.position) / 2f;

        int newLevel = Mathf.Min(level + 1, GameManager.instance.Fruits.Length - 1);
        int scoreGain = (int)Mathf.Pow(2, newLevel);
        GameObject newFruit = Instantiate(GameManager.instance.Fruits[newLevel], mergePos, Quaternion.identity);
        newFruit.name = GameManager.instance.Fruits[newLevel].name;

        GameManager.instance.PlayMergeSound(mergeSound);

        GameObject efect = Instantiate(mergeEffects[level], transform.position, Quaternion.identity);
        float efectscale = 2f + (level * 0.4f);
        efect.transform.localScale = Vector3.one * efectscale;

        Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = 1f;

        if(newFruit.GetComponent<GameControl>() != null)
        {
            Destroy(newFruit.GetComponent<GameControl>());
        }
        SpawnController.instance.ReSelectFruit();

        yield return null;

        GameManager.instance.AddScore(scoreGain * 10);

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
