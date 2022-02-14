using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public List<GameObject> hitObjects;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == ("Enemy"))
        {
            GameObject objectcollision = collision.gameObject;
            hitObjects.Add(objectcollision);
        }
    }
}
