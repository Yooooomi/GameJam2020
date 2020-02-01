using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public float moveSpeed;
    public bool ignoreDamage = false;
    public float life = 100.0f;
    private float maxLife;

    public UnityEvent onDamages = new UnityEvent();

    private void Start()
    {
        maxLife = life;
    }

    public void DoDamage(float damage)
    {
        if (ignoreDamage) return;
        onDamages.Invoke();
        life -= damage;
    }

    public void Heal(float amount)
    {
        life = Mathf.Clamp(life + amount, 0, maxLife);
    }
}
