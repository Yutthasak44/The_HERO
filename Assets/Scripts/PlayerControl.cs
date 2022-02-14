using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 6f;
    private float jumpForce = 1000;
    private float dashForce = 5.0f;
    private int xAxis;
    public int direction = 1;
    public int AttackDamage = 0;
    public float Health = 100;
    public float Mana = 100;
    public float HealthRatio = 100;
    public float ManaRatio = 100;
    private float HealthRegenTime = 0.0f;
    public float HealthRegenRate = 10.0f;
    private float ManaRegenTime = 0.0f;
    public float ManaRegenRate = 1.0f;
    public bool Stun = false;
    private float StunTime = 0.0f;
    public float StunTimeRate = 0.5f;
    private int Skill;
    public bool isGrounded;
    private bool isJumpPressed;
    private bool isDash;
    private bool Skill_1;
    private bool Skill_2;
    private float[] SkillCost = { 2, 20 };
    private Animator animator;
    private Rigidbody2D rb2d;
    public GameObject projectileBullets = null;
    public GameObject projectileBooms = null;
    public GameObject AttackZone;
    public GameObject MeleeAttackZone;
    public GameObject gameoverpanel;
    public Image HealthBar;
    public Image ManaBar;
    public string NameLevel;

    Vector2 Spawnpoint;

    [SerializeField]
    AnimatetionEventNotify AniNotify;
    [SerializeField]
    MeleeAttack AttackBehevier;
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        Spawnpoint.x = PlayerPrefs.GetFloat("pointx");
        Spawnpoint.y = PlayerPrefs.GetFloat("pointy");
        gameObject.transform.position = Spawnpoint;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HealtCal(10);
        }

        HealthRatio = Health / 100;
        ManaRatio = Mana / 100;
        float currentFillHealth = HealthBar.fillAmount;
        float currentFillMana = ManaBar.fillAmount;
        HealthBar.fillAmount = Mathf.Lerp(currentFillHealth, HealthRatio, 0.01f);
        ManaBar.fillAmount = Mathf.Lerp(currentFillMana, ManaRatio, 0.01f);

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A ) || (AniNotify.OnAttacking && isGrounded))
        {
            animator.SetBool("isRuning", false);
            xAxis = 0;
        }

        if (!Stun)
        {
            //Checking for inputs
            if (Input.GetKey(KeyCode.A) && !isDash
                && !AniNotify.OnAttacking)
            {
                xAxis = -1;
                animator.SetBool("isRuning", true);
            }
            if (Input.GetKey(KeyCode.D) && !isDash
                && !AniNotify.OnAttacking)
            {
                xAxis = 1;
                animator.SetBool("isRuning", true);
            }

            //space jump key pressed?
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDash
                && !AniNotify.OnAttacking)
            {
                isJumpPressed = true;
                isGrounded = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash && isGrounded
                 && !AniNotify.OnAttacking && xAxis != 0)
            {
                animator.SetBool("isdash", true);
                isDash = true;
                StartCoroutine(dash(direction));

            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && Mana - SkillCost[0] > 0
                && !AniNotify.OnAttacking && isGrounded && !Skill_1)
            {
                AniNotify.OnAttacking = true;
                ManaCal(-SkillCost[0]);
                Skill = 1;
                animator.SetTrigger("Skill_1");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && Mana - SkillCost[1] > 0
                && !AniNotify.OnAttacking && isGrounded && !Skill_2)
            {
                AniNotify.OnAttacking = true;
                ManaCal(-SkillCost[1]);
                Skill = 2;
                animator.SetTrigger("Skill_2");
            }

            if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1"))
                && !AniNotify.OnAttacking)
            {
                AniNotify.OnAttacking = true;
                animator.SetTrigger("isAttack");
            }
        }
        else
        {
            direction = 0;
            xAxis = 0;
        }

        IsAttacking();
        StunUpdate();
    }

    //=====================================================
    // Physics based time step loop
    //=====================================================
    private void FixedUpdate()
    {
        RecoverHP_MP();

        //Check update movement based on input
        Vector2 vel = UpdateMovement();

        //assign the new velocity to the rigidbody
        if (!isDash)
            rb2d.velocity = vel;
    }

    //check if player is on the ground
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector2 playerpos = transform.position;
            Vector2 groundpos = collision.gameObject.transform.position;
            if (playerpos.y > groundpos.y)
            {
                animator.SetBool("isground", true);
                isGrounded = true;
            }
        }

        if (collision.gameObject.tag == "health")
        {
            HealtCal(-20);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "mana")
        {
            ManaCal(20);
            Destroy(collision.gameObject);
        }

    }

    //check if player is not on the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("isground", false);
            isGrounded = false;
        }
    }

    Vector2 UpdateMovement()
    {
        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        //===RunMovement===
        if (xAxis < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            vel.x = -walkSpeed;
            direction = -1;
        }
        else if (xAxis > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            vel.x = walkSpeed;
            direction = 1;
        }
        else
        {
            vel.x = 0;
        }

        //===JumpMovement===
        if (isJumpPressed)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
        }
        return vel;
    }
    IEnumerator dash(float direction)
    {

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
        rb2d.AddForce(new Vector2(dashForce * direction, 0f), ForceMode2D.Impulse);
        
        float garvity = rb2d.gravityScale;
        rb2d.gravityScale = 0;
        yield return new WaitForSeconds(0.8f);
        isDash = false;
        rb2d.gravityScale = garvity;
        animator.SetBool("isdash", false);

    }

    public void Player_Healt(int takedamage, GameObject damageFromobject)
    {
        animator.SetBool("Stun", true);
        HealtCal(takedamage);
        Vector2 currentLocation = gameObject.transform.position;
        Vector2 damageLocation = damageFromobject.transform.position;
        if (!isDash)
        {
            if (currentLocation.x > damageLocation.x)
            {
                rb2d.AddForce(new Vector2(3200, 300));
            }
            else
            {
                rb2d.AddForce(new Vector2(-3200, 300));
            } }
        print(this.gameObject.name + ": HP : " + Health + "Frome : " + damageFromobject.name);
        if (Health <= 0)
        {
            print("die");
        }
    }

    void HealtCal(float value)
    {
        Health -= value;

        if (Health > 100)
        {
            Health = 100;
        }
        else if (Health < 0)
        {
            Health = 0;
            animator.SetBool("isDead", true);
        }
    }
    void ManaCal(float value)
    {
        Mana += value;
        if (Mana < 0)
        {
            Mana = 0;
        }
        if (Mana > 100)
        {
            Mana = 100;
        }
    }

    void Fire()
    {
        if (Skill == 1)
        {
            if (projectileBullets != null)
            {
                GameObject projectileGameObject = Instantiate(projectileBullets, transform.position, transform.rotation);
                projectileGameObject.transform.position = AttackZone.gameObject.transform.position;
            }
        }
        else
        {
            if (projectileBooms != null)
            {
                GameObject projectileGameObject = Instantiate(projectileBooms, transform.position, transform.rotation);
                projectileGameObject.transform.position = AttackZone.gameObject.transform.position;
                Rigidbody2D Item_rb2d = projectileGameObject.GetComponent<Rigidbody2D>();
                Item_rb2d.AddForce(new Vector2(0, 600));
            }
        }
    }

    void RecoverHP_MP()
    {
        if (Mana < 100)
            ManaRegenTime += Time.deltaTime;
        if (ManaRegenTime > ManaRegenRate)
        {
            ManaRegenTime = 0.0f;
            ManaCal(2);
        }

        if (Health < 100)
            HealthRegenTime += Time.deltaTime;
        if (HealthRegenTime > HealthRegenRate)
        {
            HealthRegenTime = 0.0f;
            HealtCal(-2);
        }
    }

    void IsAttacking()
    {
        if (AttackBehevier.hitObjects.Count > 0)
        {
            for (int index = 0; index < AttackBehevier.hitObjects.Count; index++)
            {
                GameObject currenObject = AttackBehevier.hitObjects[index];
                Enemy enemy = currenObject.GetComponent<Enemy>();
                enemy.Enemy_Healt(AttackDamage, this.gameObject);
            }
            AttackBehevier.hitObjects.Clear();
        }
    }

    void StunUpdate() 
    {
        if (Stun)
        {
            StunTime += Time.deltaTime;

            if(StunTime >= StunTimeRate)
            {
                StunTime = 0;
                Stun = false; 
                animator.SetBool("Stun", false);
            }
        }
    }

    void GameOver()
    {
        animator.SetBool("isDead", false);
        gameoverpanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestatGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(NameLevel);
        PlayerPrefs.SetFloat("pointx", -5);
        PlayerPrefs.SetFloat("pointy", 0);
    }
}
