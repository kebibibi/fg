using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject healthBar;
    public GameObject staminaBar;

    public GameObject player1;
    public Vector2 enemyDir;

    float horizontal;
    public float playerSpeed;

    [Range(-8,8)]
    public float maxSpeed;
    public float jumpingForce;
    public float dashForce;
    public float dashStamina;
    public float playerHealth;
    float timer;

    bool facingRight = true;
    bool grounded;
    bool jump;

    public float fistDamage;
    public float abilityDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        staminaBar.transform.localScale = new Vector3(dashStamina * -0.3f, staminaBar.transform.localScale.y);
        healthBar.transform.localScale = new Vector3(playerHealth / -100, healthBar.transform.localScale.y);

        Health();

        Controls();

        Flip();

        EnemyDirection();
    }

    private void FixedUpdate()
    {
        Jumping();
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
    }

    void Health()
    {
        if (playerHealth < 0)
        {
            enabled = false;
        }
    }

    void Controls()
    {
        if (Input.GetKey(KeyCode.Keypad6))
        {
            float smoothSpeed = Mathf.Lerp(playerSpeed, maxSpeed, 0.02f);
            playerSpeed = smoothSpeed;
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {
            float smoothSpeed = Mathf.Lerp(playerSpeed, maxSpeed, 0.02f);
            playerSpeed = smoothSpeed;
            horizontal = -1;
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
        enemyDir = player1.transform.position - transform.position;
    }
    
    public void Knockback()
    {
        rb.AddForce(enemyDir.normalized * -jumpingForce, ForceMode2D.Impulse);
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
        if(!facingRight && enemyDir.x > 0)
        {
            facingRight=true;
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
}