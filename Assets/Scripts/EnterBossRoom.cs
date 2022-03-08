using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossRoom : MonoBehaviour
{
    public GameObject target;
    public Animator animator;
    AudioSource Sound;
    public bool isOpen;

    private void Start()
    {
        Sound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isOpen)
        {
            isOpen = true;
            Open();
            Sound.Play();
        }
    }

    void Open()
    { 
        if(target.active == false)
        {
            target.SetActive(true);
        }
    }
    void Close()
    {
        if (target.active == true)
        {
            target.SetActive(false);
        }
    }

}
