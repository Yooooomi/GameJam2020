using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barel : MonoBehaviour
{
    public float damage = 30.0f;
    public float explosionRadius = 2f;
    public GameObject explosion;

    private GlobalStats gameStats;

    void Start()
    {
        gameStats = FindObjectOfType<GlobalStats>();   
    }

    void Update()
    {
        transform.position -= new Vector3(0, gameStats.mapSpeed * Time.deltaTime);
    }

    public void explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, explosionRadius);
        foreach (Collider2D collider in hitColliders)
        {
            Stats stats = collider.GetComponent<Stats>();
            if (stats != null)
            {
                stats.DoDamage(damage);
            }
        }
        GameObject expObj = Instantiate(explosion);
        expObj.transform.position = transform.position;
        expObj.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null && stats.ignoreCollision)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            return;
        }
        explode();
    }
}
