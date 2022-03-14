using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    public float health = 10;
    public float Maxhealth = 10;
    public int EnemyDamage = 10;
    private Rigidbody2D rb2d;
    public bool knockback;
    private Animator animator;
    public bool HaveAnimateDeath = true;
    public Image HealthBar;
    public float HealthRatio = 100;

    public void Start()
    {
        Player = GameObject.Find("Player");
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        health = Maxhealth;
    }

    private void Update()
    {
        if(HealthBar != null)
        {
            UpdateHpBar();
        }
    }

    void UpdateHpBar()
    {
        HealthRatio = health / Maxhealth;
        float currentFillHealth = HealthBar.fillAmount;
        HealthBar.fillAmount = Mathf.Lerp(currentFillHealth, HealthRatio, 0.01f);
    }

    public void Enemy_Healt(int takedamage, GameObject damageFromobject)
    {
        health -= takedamage;
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

        if (health <= 0)
        {
            if (HaveAnimateDeath)
            {
                animator.SetTrigger("IsDeath");
            } 
            else
            {
                death();
            }
        }

        print(this.gameObject.name + ": HP : " + health + "Frome : " + damageFromobject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (health > 0)
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
