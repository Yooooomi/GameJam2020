using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    private SideUI sideUI;
    private PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        stats.onChange.AddListener(this.UpdateStats);
    }

    public void SetUpSideUI(SideUI side)
    {
        sideUI = side;
    }

    public void UpdateStats()
    {
        if (sideUI == null) return;
        sideUI.cdr.text = "Cooldown: " + (int) stats.truckCooldownReduction + "%";
        sideUI.totalQte.text = "QTE Total: " + stats.totalQte;
    }
}
