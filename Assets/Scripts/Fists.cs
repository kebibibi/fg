using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MonoBehaviour
{
    float attackTimer;
    public float maxAttackT;
    float cooldown;
    public float maxCooldown;
    float knockTimer;
    public float maxKnockT;
    public float shake;

    bool cooling;
    bool knocked;
    bool attacking;

    Vector2 attack;
    Vector2 nAttack;
    Vector2 attackUP;

    Players playerEnemy;
    Players thisPlayer;

    public CameraFollow cam;

    private void Start()
    {
        attackTimer = 0;
        knockTimer = maxKnockT;
        attack = new Vector2(1.25f, 0.25f);
        attackUP = new Vector2(1.25f, 1f);
        nAttack = new Vector2(0.3f, 0.25f);

        thisPlayer = GetComponentInParent<Players>();
    }

    void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Timers();
    }

    void Attack()
    {
        //player1 controls
        if (this.gameObject.CompareTag("Fist1"))
        {
            if (Input.GetKeyDown(KeyCode.E) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                transform.localPosition = attack;
                attackTimer = maxAttackT;
                attacking = true;
                cooldown = maxCooldown;
            }
            if (Input.GetKeyDown(KeyCode.E) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                transform.localPosition = attackUP;
                attackTimer = maxAttackT;
                attacking = true;
                cooldown = maxCooldown;
            }
        }

        //player2 controls
        if (this.gameObject.CompareTag("Fist2"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad7) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                transform.localPosition = attack;
                attackTimer = maxAttackT;
                attacking = true;
                cooldown = maxCooldown;
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                transform.localPosition = attackUP;
                attackTimer = maxAttackT;
                attacking = true;
                cooldown = maxCooldown;
            }
        }
    }

    void Timers()
    {
        if (attacking)
        {
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer < 0)
        {
            attacking = false;
            transform.localPosition = nAttack;
            cooling = true;
        }

        if (cooling && cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (cooldown <= 0)
        {
            cooling = false;
        }

        if (knocked)
        {
            knockTimer -= Time.deltaTime;
        }
        if (knockTimer < 0)
        {
            knockTimer = maxKnockT;
            playerEnemy.enabled = true;
            cam.shakingMuch = 0;
            knocked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            playerEnemy = collision.GetComponent<Players>();

            playerEnemy.enabled = false;
            playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
            playerEnemy.playerHealth -= playerEnemy.fistDamage;

            cam.shakingMuch = 0.2f;

            knockTimer = maxKnockT;
            knocked = true;
        }
    }
}
