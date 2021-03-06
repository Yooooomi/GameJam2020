﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair : MonoBehaviour
{
    public float damage = 20.0f;
    private bool hasDamaged; 

    private SoundPlayer player;
    private GlobalStats gameStats;
    private void Start()
    {
        player = GetComponent<SoundPlayer>();
        gameStats = GameObject.FindObjectOfType<GlobalStats>();
    }

    private void Update()
    {
        transform.position -= new Vector3(0, gameStats.mapSpeed) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, Time.deltaTime * 300));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasDamaged) return;

        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null)
        {
            if (stats.ignoreCollision)
            {
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            }
            else
            {
                stats.DoDamage(damage);
                hasDamaged = true;
                player.Play();
            }
            //Vector3 force = (transform.position - collision.gameObject.transform.position).normalized;
            //force *= -1;
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 5000);
        }
    }

}
