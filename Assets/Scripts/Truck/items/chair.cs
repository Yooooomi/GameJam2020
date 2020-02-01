using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair : MonoBehaviour
{
    public float damage = 20.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CHAR IN YOUR FACE");
        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null)
        {
            stats.DoDamage(damage);

            Vector3 force = (transform.position - collision.gameObject.transform.position).normalized;

            force *= -1;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 5000);

        }
    }

}
