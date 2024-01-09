using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Rigidbody2D rb;
    BoxCollider2D bc2D;
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
    public float dashForce;
    public float dashStamina;
    public float playerHealth;
    float timer;

    bool facingRight = true;
    bool grounded;
    bool jump;
    bool dash;

    public float fistDamage;
    public float abilityDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            staminaBar.transform.localScale = new Vector3(dashStamina * 0.3f, staminaBar.transform.localScale.y);
            healthBar.transform.localScale = new Vector3(playerHealth / 100, healthBar.transform.localScale.y);
        }

        if (this.gameObject.CompareTag("Player2"))
        {
            staminaBar.transform.localScale = new Vector3(dashStamina * -0.3f, staminaBar.transform.localScale.y);
            healthBar.transform.localScale = new Vector3(playerHealth / -100, healthBar.transform.localScale.y);
        }
        
        Health();

        Controls();

        EnemyDirection();
    }

    private void FixedUpdate()
    {
        Jumping();

        Dash();

        Flip();

        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
    }

    void Health()
    {
        if (playerHealth < 0)
        {
            fist = GetComponentInChildren<Fists>();
            fist.enabled = false;
            enabled = false;
        }
    }

    void Controls()
    {
        //player1 controls
        if (this.gameObject.CompareTag("Player1"))
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashStamina > 0 && playerSpeed > 0)
            {
                dash = true;
            }
        }

        //player2 controls
        if (this.gameObject.CompareTag("Player2"))
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

            if (Input.GetKeyDown(KeyCode.KeypadEnter) && dashStamina > 0 && playerSpeed > 0)
            {
                dash = true;
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

    void Dash()
    {
        if (dash)
        {
            playerSpeed = dashForce;
            dashStamina--;
            timer = 1;
        }

        if (dashStamina < 3)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0)
            {
                dashStamina++;
                timer = 1;
            }
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
        grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            bc2D.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            bc2D.enabled = true;
        }
    }
}
