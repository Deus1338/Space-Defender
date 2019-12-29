using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        int quantity = FindObjectsOfType<BackgroundMusic>().Length;
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

    void Update()
    {
        
    }
}
