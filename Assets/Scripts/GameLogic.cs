using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public List<SideUI> sideUIs;
    public float slowMoMultiplier;
    public AnimationCurve qteScoreToHealth;
    public AnimationCurve qteScoreCooldownReduction;

    public float phaseTime;
    public float qteTime;
    public float currentPhaseTime { get; private set; }
    public bool playerOneOnBike { get; private set; } = true;
    private float currentQteTime;

    private RoleManager[] players;
    private qte[] qtes;

    private bool isChangingPhase;
    private bool initing = true;

    private void Start()
    {
        players = FindObjectsOfType<RoleManager>();
        qtes = FindObjectsOfType<qte>();
    }

    private void ChangePhase(bool qte = true)
    {
        currentPhaseTime = 0;
        isChangingPhase = true;

        players[0].ClearVehicle();
        players[1].ClearVehicle();

        currentQteTime = 0;
        if (qte)
        {
            SlowMoManager.Instance.SetSlowMo(slowMoMultiplier);
            for (int i = 0; i < 2; i++)
            {
                qtes[i].StartQTE(players[i].GetComponent<Inputs>());
            }
        }
        else
        {
            currentQteTime = qteTime + 1;
        }
        playerOneOnBike = !playerOneOnBike;
    }

    private void EndChangingPhase()
    {
        int healIndex = playerOneOnBike ? 0 : 1;
        int cdIndex = playerOneOnBike ? 1 : 0;

        int healScore = qtes[healIndex].End();
        GameObject.Find("Bike").GetComponent<Stats>().Heal(qteScoreToHealth.Evaluate((float) healScore));

        int cdScore = qtes[cdIndex].End();

        PlayerStats stats = players[healIndex].GetComponent<PlayerStats>();
        stats.SetTotalQTE(healScore);

        stats = players[cdIndex].GetComponent<PlayerStats>();
        stats.AddCdReduction(qteScoreCooldownReduction.Evaluate((float) cdScore));
        stats.SetTotalQTE(cdScore);
        
        isChangingPhase = false;

        if (!initing)
        {
            SlowMoManager.Instance.SetSlowMo(1);
            SpawnSplashText.Instance.Spawn("Bike repaired! " + healScore + " hits!", qtes[healIndex].transform.position);
            SpawnSplashText.Instance.Spawn("Cooldowns reduced! " + cdScore + " hits!", qtes[cdIndex].transform.position);
        }

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
        initing = false;
    }

    public void StartGame()
    {
        currentPhaseTime = 0;
        ChangePhase(false);
    }

    private void Update()
    {
        if (isChangingPhase)
        {
            currentQteTime += Time.unscaledDeltaTime;
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
