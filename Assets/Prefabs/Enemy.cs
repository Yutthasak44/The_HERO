using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    public int healt = 10;
    public int EnemyDamage = 10;
    public Rigidbody2D rb2d;
    public bool knockback;

//<<<<<<< HEAD=======
    public float movementspeed = 2.0f;
    public float detectRange = 10.0f;
    public float maximumRange = 10.0f;
    Vector3 origin;

//>>>>>>> 9423f98c691d700f8dc3ddbbfdd5187aa3dbc106
    public void Start()
    {
        Player = GameObject.Find("Player");

        origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update()
    {
        float deltaPosition = Vector2.Distance(Player.transform.position, gameObject.transform.position);
        float originPosition = origin.x + (Mathf.PingPong(Time.time * movementspeed, maximumRange) - maximumRange / 2);
        if (deltaPosition < detectRange)
        {
            AttackPlayer();
        }
        else
        {
            if (Mathf.Abs(transform.position.x - originPosition) > 0.01f) WalkBack(originPosition);
            Patrol(originPosition);
        }
    }

    void AttackPlayer()
    {
        transform.LookAt(Player.transform);
        transform.position = new Vector3(transform.position.x + (movementspeed * Time.deltaTime * transform.rotation.eulerAngles.normalized.y), transform.position.y, transform.position.z);
    }

    void Patrol(float position)
    {
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }

    void WalkBack(float position)
    {
        transform.LookAt(new Vector3(position, transform.position.y, transform.position.z));
        transform.position = new Vector3(transform.position.x + (movementspeed * Time.deltaTime * transform.rotation.eulerAngles.normalized.y), transform.position.y, transform.position.z);
    }


    public void Enemy_Healt(int takedamage, GameObject damageFromobject)
    {

        healt -= takedamage;
        Vector2 currentLocation = gameObject.transform.position;
        Vector2 damageLocation = damageFromobject.transform.position;

        if (!knockback)
        { 
            if (currentLocation.x > damageLocation.x)
            {
                rb2d.AddForce(new Vector2(120, 100));
            }
            else
            {
                rb2d.AddForce(new Vector2(-120, 100));
            } 
        }

        print(this.gameObject.name + ": HP : " + healt + "Frome : " + damageFromobject.name);
        if (healt <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerControl player = Player.GetComponent<PlayerControl>();
            player.Stun = true;
            player.Player_Healt(10, this.gameObject);
        }
    }
}
