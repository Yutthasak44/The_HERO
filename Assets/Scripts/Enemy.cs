using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    public int healt = 10;
    public int EnemyDamage = 10;
    public Rigidbody2D rb2d;
    public bool knockback;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    public void Enemy_Healt(int takedamage, GameObject damageFromobject)
    {

        healt -= takedamage;
        Vector2 currentLocation = gameObject.transform.position;
        Vector2 damageLocation = damageFromobject.transform.position;

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

        print(this.gameObject.name + ": HP : " + healt + "Frome : " + damageFromobject.name);
        if (healt <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerControl player = Player.GetComponent<PlayerControl>();
            player.Stun = true;
            player.Player_Healt(10, this.gameObject);
        }
    }
}
