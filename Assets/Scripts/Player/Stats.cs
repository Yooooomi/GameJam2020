using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float moveSpeed;
// public float life { get; private set; }
    public float life = 100.0f;

    public void DoDamage(float damage)
    {
        life -= damage;
    }
}
