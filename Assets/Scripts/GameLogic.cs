using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public float phaseTime;
    private float currentPhaseTime;
    private bool playerOneOnBike = true;

    private RoleManager[] players;

    private void Start()
    {
        players = GameObject.FindObjectsOfType<RoleManager>();
    }

    private void ChangePhase()
    {
        // TODO do QTE

        playerOneOnBike = !playerOneOnBike;

        if (playerOneOnBike)
        {
            players[0].SetOnBike();
            players[1].SetInTruck();
        }
        else
        {
            players[0].SetInTruck();
            players[1].SetOnBike();
        }
    }

    private void Update()
    {
        currentPhaseTime += Time.deltaTime;

        if (currentPhaseTime > phaseTime)
        {
            currentPhaseTime = 0;
            ChangePhase();
        }
    }
}
