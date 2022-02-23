using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject target;
    GameObject Player;
    public Animator animator;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Attack")
        {
            if (target != null)
            {
                animator.enabled = true;
                Destroy(target);
            }
        }
    }

    void Setanimate()
    {
        animator.enabled = false;
    }

}
