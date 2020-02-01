using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public float damage = 50.0f;
    public float explosionRadius = 2f;
    public GameObject explosion;

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

}
