using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public AnimationCurve qteScoreToHealth;
    public AnimationCurve qteScoreCooldownReduction;

    public float phaseTime;
    public float qteTime;
    public float currentPhaseTime { get; private set; }
    private bool playerOneOnBike = true;
    private float currentQteTime;

    private RoleManager[] players;
    private qte[] qtes;

    private bool isChangingPhase;

    private void Start()
    {
        players = GameObject.FindObjectsOfType<RoleManager>();
        qtes = GameObject.FindObjectsOfType<qte>();
    }

    private void ChangePhase()
    {
        currentPhaseTime = 0;
        isChangingPhase = true;

        players[0].ClearVehicle();
        players[1].ClearVehicle();

        currentQteTime = 0;
        for (int i = 0; i < 2; i++)
        {
            qtes[i].StartQTE(players[i].GetComponent<Inputs>());
        }

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

    private void EndChangingPhase()
    {
        int healIndex = playerOneOnBike ? 0 : 1;
        int cdIndex = playerOneOnBike ? 1 : 0;

        float score = (float) qtes[healIndex].End();
        GameObject.Find("Bike").GetComponent<Stats>().Heal(qteScoreToHealth.Evaluate(score));

        score = (float)qtes[cdIndex].End();
        players[cdIndex].GetComponent<PlayerStats>().AddCdReduction(qteScoreCooldownReduction.Evaluate(score));
    }

    public void StartGame()
    {
        currentPhaseTime = 0;
        ChangePhase();
    }

    private void Update()
    {
        if (isChangingPhase)
        {
            currentQteTime += Time.deltaTime;
            if (currentQteTime > qteTime)
            {
                EndChangingPhase();
            }
            return;
        }
        currentPhaseTime += Time.deltaTime;

        if (currentPhaseTime > phaseTime)
        {
            ChangePhase();
        }
    }
}
