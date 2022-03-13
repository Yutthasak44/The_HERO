using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveMent : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    PlayerControl player;
    public int EnemyDamage = 10;
    private Rigidbody2D rb2d;
    private Animator animator;

    public bool knockback;
    public bool isCanMove = false;

    public bool Stun;
    private bool Isattack;
    private float StunTime = 0.0f;
    public float StunTimeRate = 0.5f;
    public float movementspeed = 2.0f;
    public float distancnAttack = 0;
    public int Counting;
    public int NextAttack;

    public void Start()
    {
        Player = GameObject.Find("Player");
        player = Player.GetComponent<PlayerControl>();
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Update_FacingToPlayer();
        if (!Stun && !Isattack && isCanMove)
        {
            Update_ChasingToPlayer();
        }
        else if (!Isattack && isCanMove)
        {
            StunUpdate();
        }
    }

    void StunUpdate()
    {
        if (Stun)
        {
            StunTime += Time.deltaTime;

            if (StunTime >= StunTimeRate)
            {
                StunTime = 0;
                Stun = false;
            }
        }
    }

    void Update_FacingToPlayer() 
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 enemyPos = this.gameObject.transform.position;

        if(enemyPos.x > playerPos.x)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(enemyPos.x < playerPos.x)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Update_ChasingToPlayer()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 enemyPos = this.gameObject.transform.position;

        if (enemyPos.x > playerPos.x + distancnAttack)
        {
            rb2d.velocity = new Vector2(-movementspeed, rb2d.velocity.y);
            animator.SetBool("IsWalk", true);
        }
        else if (enemyPos.x < playerPos.x - distancnAttack)
        {
            rb2d.velocity = new Vector2(movementspeed, rb2d.velocity.y);
            animator.SetBool("IsWalk", true);
        }
        else if (player.isGrounded == true)
        {
            Isattack = true;
            animator.SetTrigger("IsAttack");
            animator.SetBool("IsWalk", false);
        }
    }

    public void WaitingForNextAttack()
    {
        Counting++;
        if (Counting == 2)
        {
            Counting = 0;
            Isattack = false;
        }
    }
}
