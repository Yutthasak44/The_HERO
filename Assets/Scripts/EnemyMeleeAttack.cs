using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            PlayerControl player = Player.GetComponent<PlayerControl>();
            player.Stun = true;
            player.Player_Healt(10, this.gameObject);
        }
    }
}
