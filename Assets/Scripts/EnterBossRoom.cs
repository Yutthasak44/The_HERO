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
        if(target == null)
            target = GameObject.Find("DoorExit");
        Sound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isOpen)
        {
            isOpen = true;
            Close();
            
        }
    }

    public void Open()
    {
        if (target != null)
        { 
            target.SetActive(false);
            Sound.Play();
        }
    }

    public void Close()
    {
        if (target != null)
        {
            target.SetActive(true);
            Sound.Play();
        }
    }

}
