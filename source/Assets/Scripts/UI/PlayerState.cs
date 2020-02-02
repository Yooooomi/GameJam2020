using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public GameObject connected;
    public GameObject disconnected;
    public Image img;
    public Sprite notConnected;
    public Sprite keyboard;
    public Sprite controller;

    public void connect(Inputs.ControllerType type)
    {
        img.sprite = type == Inputs.ControllerType.CONTROLLER ? controller : keyboard;
        disconnected.SetActive(false);
        connected.SetActive(true);
    }

    public void disconnect()
    {
        img.sprite = notConnected;
        disconnected.SetActive(true);
        connected.SetActive(false);
    }
}
