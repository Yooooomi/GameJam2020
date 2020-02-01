using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float moveSpeed;
    public bool ignoreDamage = false;
// public float life { get; private set; }
    public float life = 100.0f;
    private float maxLife;

    private void Start()
    {
        maxLife = life;
    }

    public void DoDamage(float damage)
    {
        if (ignoreDamage) return;
        life -= damage;
    }

    public void Heal(float amount)
    {
        life = Mathf.Clamp(life + amount, 0, maxLife);
    }
}
