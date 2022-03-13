using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public List<GameObject> hitObjects;
    public int AttackDamage;

    private void Update()
    {
        IsAttacking();
    }

    void IsAttacking()
    {
        if (hitObjects.Count > 0)
        {
            for (int index = 0; index < hitObjects.Count; index++)
            {
                GameObject currenObject = hitObjects[index];
                EnemyHealth enemy = currenObject.GetComponent<EnemyHealth>();
                enemy.Enemy_Healt(AttackDamage, this.gameObject);
            }
            hitObjects.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            GameObject objectcollision = collision.gameObject;
            hitObjects.Add(objectcollision);
        }
    }
}

