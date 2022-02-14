using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("The distance this projectile will move each second.")]
    public float projectileSpeed = 10.0f;
    public float gravity = 10.0f;
    public GameObject BomeEffect;
    public bool BomeDestroy;

    Rigidbody2D rb2d;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {          
            Destroy(this.gameObject);
            if(BomeEffect != null && !BomeDestroy)
            {
                BomeDestroy = true;
                GameObject projectileGameObject = Instantiate(BomeEffect, transform.position, transform.rotation);
            }
        }
        if (collision.gameObject.tag == "Enemy" && BomeEffect != null && !BomeDestroy)
        {
            Destroy(this.gameObject);
            if (BomeEffect != null && !BomeDestroy)
            {
                BomeDestroy = true;
                GameObject projectileGameObject = Instantiate(BomeEffect, transform.position, transform.rotation);
            }
        }
    }

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }
}