using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public List<GameObject> Item;
    private float RamdomDropRate;
    public float PercentDropRate;
    private float RamdomItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Attack")
        {
            RamdomDropRate = Random.Range(0, 100);
            if (RamdomDropRate < PercentDropRate)
            {
                RamdomItem = Random.Range(0, Item.Count);
                GameObject DropItem;
                Rigidbody2D Item_rb2d;
                int force;
                for (int i = 0; i < Item.Count; i++)
                {
                    if (RamdomItem == i)
                    {
                        print(RamdomItem);
                        DropItem = Instantiate(Item[i], transform.position, transform.rotation);
                        Item_rb2d = DropItem.GetComponent<Rigidbody2D>();
                        force = Random.Range(-1, 2);
                        Item_rb2d.AddForce(new Vector2(force * 50, 300));
                        break;
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
