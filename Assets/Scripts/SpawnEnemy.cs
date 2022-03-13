using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject Target;
    GameObject Object;
    public bool isboss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SpawnEnemy")
        {
            if(Object == null && !isboss)
                Object = Instantiate( Target,transform.position, transform.rotation);
            else
            {
                Object = Instantiate(Target, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
