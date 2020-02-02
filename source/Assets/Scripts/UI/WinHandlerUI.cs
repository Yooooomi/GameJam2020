using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinHandlerUI : MonoBehaviour
{
    public Text text;
    
    private Joiner joiner;

    private void Start()
    {
        joiner = FindObjectOfType<Joiner>();

        text.text = "Player " + joiner.lastWinner + " won!";
    }

    public void StartAgain()
    {
        FindObjectOfType<Joiner>().StartTheGame();
    }
}
