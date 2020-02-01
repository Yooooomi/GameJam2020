using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeTwoOne : MonoBehaviour
{
    public Text text;
    private int current = 3;

    private GameLogic logic;

    private void Start()
    {
        logic = FindObjectOfType<GameLogic>();
    }

    public void SetNext()
    {
        current -= 1;
        if (current == 0)
        {
            text.text = "Go!!";
        }
        else if (current == -1)
        {
            logic.StartGame();
            Destroy(text);
        }
        else
        {
            text.text = current.ToString();
        }
    }
}
