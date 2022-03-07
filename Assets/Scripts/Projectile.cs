using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("The distance this projectile will move each second.")]
    public float projectileSpeed = 10.0f;
    public float gravity = 10.0f;
    public GameObject ObjectEffect;
    public bool BomeDestroy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            if (ObjectEffect != null && !BomeDestroy)
            {
                GameObject projectileGameObject = Instantiate(ObjectEffect, transform.position, transform.rotation);
                BomeDestroy = true;
            }
        }

        if (collision.gameObject.tag == "Enemy" && ObjectEffect != null && !BomeDestroy)
        {
            Destroy(this.gameObject);
            if (ObjectEffect != null && !BomeDestroy)
            {
                GameObject projectileGameObject = Instantiate(ObjectEffect, transform.position, transform.rotation);
                BomeDestroy = true;
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