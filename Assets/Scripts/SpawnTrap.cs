using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrap : MonoBehaviour
{
    public GameObject Target;
    GameObject Object;
    public bool start;
    float spawntime;
    public float nextspawntime = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpawnEnemy")
        {
            start = true;
            Object = Instantiate(Target, transform.position, transform.rotation);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpawnEnemy")
        {
            start = false;
        }
    }

    void Update()
    {
        if (start)
        {
            spawntime += Time.deltaTime;
            if (spawntime >= nextspawntime)
            {
                spawntime = 0;
                Object = Instantiate(Target, transform.position, transform.rotation);
            }
        }
    }
}
