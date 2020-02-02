using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityManager : MonoBehaviour
{
    private Stats stats;

    public SpriteRenderer render;

    public float invincibleTime;
    public float blinkTime;

    private float currentInvincible;
    private float currentBlink;
    private bool hidden;
    private Color initialColor;

    private void Start()
    {
        if (render != null)
        {
            initialColor = render.color;
        }
        stats = GetComponent<Stats>();
        stats.onDamages.AddListener(this.OnDamages);
    }

    private void UpdateHidden()
    {
        if (render == null) return;
        if (hidden)
        {
            Color c = render.color;
            c.a = 0.3f;
            render.color = c;
        }
        else
        {
            render.color = initialColor;
        }
    }


    private void OnDamages(float damages, bool notTrigger)
    {
        if (notTrigger) return;
        UpdateInvincibility(true);
        currentInvincible = 0;
        currentBlink = 0;
    }

    private void UpdateInvincibility(bool status)
    {
        stats.ignoreCollision = status;
        stats.ignoreDamage = status;
    }

    private void Update()
    {
        currentInvincible += Time.deltaTime;
        currentBlink += Time.deltaTime;

        if (stats.ignoreDamage && currentInvincible > invincibleTime)
        {
            UpdateInvincibility(false);
            currentInvincible = 0;
            hidden = false;
            UpdateHidden();
        }
        if (currentBlink > blinkTime && stats.ignoreDamage)
        {
            hidden = !hidden;
            UpdateHidden();
        }
    }
}
