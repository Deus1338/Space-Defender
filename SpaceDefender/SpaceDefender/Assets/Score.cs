using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        int quantity = FindObjectsOfType(GetType()).Length;
        if (quantity > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}
