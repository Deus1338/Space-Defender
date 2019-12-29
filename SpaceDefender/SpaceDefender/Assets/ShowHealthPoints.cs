using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHealthPoints : MonoBehaviour
{
    Player player;
    ShowHealthPoints showHealthPoints;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        showHealthPoints = FindObjectOfType<ShowHealthPoints>();
    }

    private void Update()
    {
        showHealthPoints.GetComponent<TextMeshProUGUI>().text = player.GetHealth().ToString();
    }
}
