using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    public int healt = 10;
    public int EnemyDamage = 10;
    private Rigidbody2D rb2d;
    public bool knockback;
    private Animator animator;

    public void Start()
    {
        Player = GameObject.Find("Player");
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void Enemy_Healt(int takedamage, GameObject damageFromobject)
    {
        healt -= takedamage;
        Vector2 currentLocation = gameObject.transform.position;
        Vector2 damageLocation = damageFromobject.transform.position;
        EnemyMoveMent Enemymove = this.gameObject.GetComponent<EnemyMoveMent>();
        Enemymove.Stun = true;

        if (!knockback)
        {
            if (currentLocation.x > damageLocation.x)
            {
                rb2d.AddForce(new Vector2(120, 100));
            }
            else
            {
                rb2d.AddForce(new Vector2(-120, 100));
            }
        }

        if (healt <= 0)
        {
            animator.SetTrigger("IsDeath");
        }

        print(this.gameObject.name + ": HP : " + healt + "Frome : " + damageFromobject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (healt > 0)
            {
                PlayerControl player = Player.GetComponent<PlayerControl>();
                player.Stun = true;
                player.Player_Healt(10, this.gameObject);
            }
        }
    }

    public void death()
    {
        Destroy(this.gameObject);
    }
}
