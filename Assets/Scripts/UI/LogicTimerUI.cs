using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicTimerUI : MonoBehaviour
{
    public float animThreshold;
    public Text text;
    private Animator animator;

    private float lastTimeLeft;
    private GameLogic logic;

    private void Start()
    {
        animator = GetComponent<Animator>();
        logic = FindObjectOfType<GameLogic>();
    }

    private void Update()
    {
        float timeLeft = logic.phaseTime - logic.currentPhaseTime;

        if (lastTimeLeft > animThreshold && timeLeft < animThreshold)
        {
            animator.SetBool("blinking", true);
        }
        if (lastTimeLeft < timeLeft)
        {
            animator.SetBool("blinking", false);
        }

        text.text = ((int)(timeLeft)).ToString();
        lastTimeLeft = timeLeft;
    }
}
