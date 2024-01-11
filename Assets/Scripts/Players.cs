using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D collisionHitbox;
    public BoxCollider2D triggerHitbox;
    public GameObject staminaBar;
    public GameObject healthBar;

    public GameObject playerEnemy;
    public Vector2 enemyDir;

    Fists fist;

    float horizontal;
    public float playerSpeed;

    [Range(-8, 8)]
    public float maxSpeed;
    public float jumpingForce;
    public float playerHealth;

    public float maxDashForce;
    public float maxDashTimer;
    public float dashStamina;
    float dashForce;
    float dashRefreshTimer;
    float dashTimer;

    public bool facingRight = true;
    bool grounded;
    bool jump;
    bool dashing;

    public float fistDamage;
    public float abilityDamage;

    string player1 = "Player1";
    string player2 = "Player2";
    string maincamString = "MainCamera";
    string platformString = "Platform";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject.CompareTag(player1))
        {
            staminaBar.transform.localScale = new Vector2(dashStamina * 0.3f, staminaBar.transform.localScale.y);
            healthBar.transform.localScale = new Vector2(playerHealth / 100, healthBar.transform.localScale.y);

            if (healthBar.transform.localScale.x <= 0)
            {
                healthBar.transform.localScale = new Vector2(0, healthBar.transform.localScale.y);
            }
        }

        if (gameObject.CompareTag(player2))
        {
            staminaBar.transform.localScale = new Vector3(dashStamina * -0.3f, staminaBar.transform.localScale.y);
            healthBar.transform.localScale = new Vector3(playerHealth / -100, healthBar.transform.localScale.y);

            if (healthBar.transform.localScale.x >= 0)
            {
                healthBar.transform.localScale = new Vector2(0, healthBar.transform.localScale.y);
            }
        }
        
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
            }

            if (dashTimer <= 0)
            {
                dashForce = 0;
                dashRefreshTimer = 1;
                dashing = false;
            }
        }

        if (dashStamina < 3)
        {
            if (dashRefreshTimer > 0)
            {
                dashRefreshTimer -= Time.deltaTime;
            }
            else if (dashRefreshTimer <= 0)
            {
                dashStamina++;
                dashRefreshTimer = 1;
            }
        }
    }

    void Health()
    {
        if (playerHealth <= 0 && healthBar.transform.localScale.x == 0)
        {
            fist = GetComponentInChildren<Fists>();
            fist.enabled = false;
            enabled = false;
        }
    }

    void Controls()
    {
        //player1 controls
        if (gameObject.CompareTag(player1))
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
                dashing = true;
                dashTimer = maxDashTimer;
                dashStamina--;
            }
        }

        //player2 controls
        if (gameObject.CompareTag(player2))
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(platformString))
        {
            collisionHitbox.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(platformString))
        {
            collisionHitbox.enabled = true;
        }
    }
}