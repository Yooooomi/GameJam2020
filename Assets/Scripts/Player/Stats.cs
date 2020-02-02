using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public float moveSpeed;
    public bool ignoreDamage = false;
    public bool ignoreCollision = false;
    public float life = 100.0f;
    private float maxLife;

    public class OnDamages : UnityEvent<float, bool> { }

    public OnDamages onDamages = new OnDamages();

    private void Start()
    {
        maxLife = life;
    }

    public void DoDamage(float damage, bool doNotTriggerInvincibility = false)
    {
        if (ignoreDamage) return;
        onDamages.Invoke(damage, doNotTriggerInvincibility);
        life -= damage;
    }

    public void Heal(float amount)
    {
        life = Mathf.Clamp(life + amount, 0, maxLife);
    }
}
