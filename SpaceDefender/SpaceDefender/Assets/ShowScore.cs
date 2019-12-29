using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowScore : MonoBehaviour
{
    Score scoreObject;

    
    private void Start()
    {
        scoreObject = FindObjectOfType<Score>();
    }
    private void Update()
    {
        //if(SceneManager.GetActiveScene().buildIndex != 0)
        //{
            this.GetComponent<TextMeshProUGUI>().text = scoreObject.GetScore().ToString();
        //}
    }
}
