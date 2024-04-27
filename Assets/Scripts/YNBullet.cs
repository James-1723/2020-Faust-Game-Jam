using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNBullet : MonoBehaviour
{

    public Rigidbody2D _Rigidbody = null;

    private float F = 20;

   public void Shoot(Vector2 vec)
    {
        _Rigidbody.velocity = vec * 20f;

        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
