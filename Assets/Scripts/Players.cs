using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Players : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D collisionHitbox;
    public ParticleSystem particles;
    AudioSource audios;

    public GameObject staminaBar;
    public GameObject healthBar;
    public GameObject modeBar;

    bool thisPlayer1;
    bool thisPlayer2;

    //enemy

    public GameObject playerEnemy;
    public Vector2 enemyDir;

    //scales

    Vector2 staminaScale;
    Vector2 healthScale;
    Vector2 deadScale;

    public bool dead;

    public GameMaster master;

    //weapons
    staffAttack staff;
    bool staffMode;

    Gun gun;
    bool gunMode;

    TigerStyle tiger;
    bool tigerMode;

    //playermovement

    float horizontal;
    public float playerSpeed;

    [Range(-14, 14)]
    public float maxSpeed;
    public float jumpingForce;
    public float playerHealth;

    public float maxDashForce;
    public float maxDashTimer;
    public float dashStamina;
    public float dashForce;
    public float dashRefreshTimer;
    float dashTimer;

    public bool facingRight = true;
    public bool grounded;
    bool jump;
    public bool dashing;

    //text

    public TMP_Text damageText;

    string player1 = "Player 1";
    string player2 = "Player 2";
    string maincamString = "MainCamera";
    string platformString = "Platform";

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        particles = GetComponent<ParticleSystem>();
        audios = GetComponent<AudioSource>();

        //var staff = gameObject.transform.Find("staff(Clone)").GetComponentInChildren<staffAttack>();

        if (gameObject.transform.Find("staff(Clone)") == true)
        {
            staff = GetComponentInChildren<staffAttack>();
            staffMode = true;
        }

        if (gameObject.transform.Find("Gun(Clone)") == true)
        {
            gun = GetComponentInChildren<Gun>();
            gunMode = true;
        }

        if (gameObject.transform.Find("TigerStyle(Clone)") == true)
        {
            tiger = GetComponentInChildren<TigerStyle>();
            tigerMode = true;
        }

        thisPlayer1 = gameObject.CompareTag(player1);
        thisPlayer2 = gameObject.CompareTag(player2);
    }


    void Update()
    {
        staminaScale = new Vector2(dashStamina * 0.3f, staminaBar.transform.localScale.y);
        healthScale = new Vector2(playerHealth / 100, healthBar.transform.localScale.y);

        deadScale = new Vector2(0, healthBar.transform.localScale.y);

        StatusBars();
        
        Health();

        Controls();

        EnemyDirection();
    }

    private void FixedUpdate()
    {
        float dashDir = dashForce * horizontal;

        Jumping();

        Flip();

        rb.velocity = new Vector2(horizontal * playerSpeed /*+ dashDir*/, rb.velocity.y);

        //dash
        if (dashing)
        {
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                dashForce = maxDashForce;
                rb.AddForce(Vector2.right * dashDir, ForceMode2D.Impulse);
                dashRefreshTimer = 1;
            }

            else
            {
                dashForce = 0;
                dashing = false;
            }
        }

        if (dashStamina < 3)
        {
            if (dashRefreshTimer > 0)
            {
                dashRefreshTimer -= Time.deltaTime;
            }
            else
            {
                dashStamina++;
                dashRefreshTimer = 1;
            }
        }
    }

    void StatusBars()
    {
        if (thisPlayer1)
        {
            staminaBar.transform.localScale = staminaScale;
            healthBar.transform.localScale = healthScale;

            if (staffMode)
            {
                modeBar.transform.localScale = new Vector2(staff.cooldown * 0.2f, modeBar.transform.localScale.y);
            }
            if (gunMode)
            {
                modeBar.transform.localScale = new Vector2(gun.cooldown * 0.2f, modeBar.transform.localScale.y);
            }
            if (tigerMode)
            {
                modeBar.transform.localScale = new Vector2(tiger.modeTimer * 0.1f, modeBar.transform.localScale.y);
            }

            if (healthBar.transform.localScale.x <= 0)
            {
                healthBar.transform.localScale = deadScale;
            }
        }

        if (thisPlayer2)
        {
            staminaBar.transform.localScale = -staminaScale;
            healthBar.transform.localScale = -healthScale;

            if (staffMode)
            {
                modeBar.transform.localScale = new Vector2(staff.cooldown * -0.2f, modeBar.transform.localScale.y);
            }
            if (gunMode)
            {
                modeBar.transform.localScale = new Vector2(gun.cooldown * -0.2f, modeBar.transform.localScale.y);
            }
            if (tigerMode)
            {
                modeBar.transform.localScale = new Vector2(tiger.modeTimer * -0.1f, modeBar.transform.localScale.y);
            }

            if (healthBar.transform.localScale.x >= 0)
            {
                healthBar.transform.localScale = deadScale;
            }
        }
    }

    void Health()
    {
        if (playerHealth <= 0)
        {
            dead = true;
        }
    }

    void Controls()
    {
        //player1 controls
        if (thisPlayer1)
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
                playerSpeed = maxSpeed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
                playerSpeed = maxSpeed;
            }
            else
            {
                playerSpeed = 0;
            }

            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                jump = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashStamina > 0 && playerSpeed > 0 && !dashing)
            {
                audios.Play();
                dashing = true;
                dashTimer = maxDashTimer;
                dashStamina--;
            }
        }

        //player2 controls
        if (thisPlayer2)
        {
            if (Input.GetKey(KeyCode.Keypad6))
            {
                horizontal = 1;
                playerSpeed = maxSpeed;
            }
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                horizontal = -1;
                playerSpeed = maxSpeed;
            }
            else
            {
                playerSpeed = 0;
            }

            if (Input.GetKeyDown(KeyCode.Keypad8) && grounded)
            {
                jump = true;
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) && dashStamina > 0 && playerSpeed > 0 && !dashing)
            {
                audios.Play();
                dashing = true;
                dashTimer = maxDashTimer;
                dashStamina--;
            }
        }
    }

    void Jumping()
    {
        if (jump)
        {
            rb.AddForce(transform.up * jumpingForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void EnemyDirection()
    {
        enemyDir = playerEnemy.transform.position - transform.position;
    }

    void Flip()
    {
        if (facingRight && enemyDir.x < 0)
        {
            facingRight = false;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (!facingRight && enemyDir.x > 0)
        {
            facingRight = true;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(maincamString))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(maincamString))
        {
            grounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag(platformString))
        {
            collisionHitbox.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag(platformString))
        {
            collisionHitbox.enabled = true;
        }
    }
}