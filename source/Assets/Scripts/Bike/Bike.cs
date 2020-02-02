using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bike : MonoBehaviour
{
    private Stats stats;
    private Joiner joiner;

    private void Start()
    {
        joiner = FindObjectOfType<Joiner>();
        stats = GetComponent<Stats>();
    }

    private void Update()
    {
        Debug.Log("Win !");
        if (stats.life <= 0)
        {
            joiner.EndTheGame();
            Destroy(this);
        }
    }
}
