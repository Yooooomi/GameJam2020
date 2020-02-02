using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerJoins : MonoBehaviour
{
    public Sprite notConnected;
    public Sprite keyboard;
    public Sprite controller;

    public GameObject waiting;
    public GameObject start;

    public List<PlayerState> connections;
    private int nbPlayers;

    private void Start()
    {
        start.SetActive(false);
    }

    public void OnPlayerJoins(bool connects, int index, Inputs.ControllerType type)
    {
        if (connects)
        {
            nbPlayers += 1;
            connections[index].connect(type);
        }
        else
        {
            nbPlayers -= 1;
            connections[index].disconnect();
        }
        waiting.SetActive(nbPlayers != 2);
        start.SetActive(nbPlayers == 2);
    }
}
