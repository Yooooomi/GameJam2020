using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public float pikeSlowRatio = 0.5f;
    public float damagePerSec = 20.0f;
    private GlobalStats stats;

    void Start()
    {
        stats = FindObjectOfType<GlobalStats>();
    }

    void Update()
    {
        transform.position -= new Vector3(0, stats.mapSpeed) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null)
        {
            stats.moveSpeed *= pikeSlowRatio;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null)
        {
            stats.moveSpeed *= 1 / pikeSlowRatio;
        }
        collision.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null && !stats.ignoreCollision)
        {
            stats.DoDamage(damagePerSec * Time.deltaTime, true);
            var collisionRotation = collision.gameObject.transform.rotation;
            collision.gameObject.transform.rotation = Quaternion.Euler(collisionRotation.x, collisionRotation.y, collisionRotation.z + Random.Range(-90.0f, 90.0f) * Time.deltaTime * 2);
        }
       
    }

}
