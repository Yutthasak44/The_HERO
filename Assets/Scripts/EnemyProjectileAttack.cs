using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttack : MonoBehaviour
{
    public int AttackDamage;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl player = Player.GetComponent<PlayerControl>();
            player.Stun = true;
            player.Player_Healt(AttackDamage, this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
