using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackfist2 : MonoBehaviour
{
    float attackTimer;
    public float maxAttackT;
    float cooldown;
    public float maxCooldown;
    float knockTimer;
    public float maxKnockT;

    public bool cooling;
    public bool knocked;
    public bool knockShake;

    Vector2 attack;
    Vector2 nAttack;
    Vector2 attackUP;

    Player1 player1;
    public Player2 player2;

    public float shakeDuration;
    public float maxShakeDuration;
    public float shakeAmount;
    public Vector2 shakeXY;
    public CameraFollow cam;

    private void Start()
    {
        attackTimer = 0;
        knockTimer = maxKnockT;
        attack = new Vector2(1.25f, 0.25f);
        attackUP = new Vector2(1.25f, 1f);
        nAttack = new Vector2(0.3f, 0.25f);
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
        if (Input.GetKeyDown(KeyCode.Keypad7) && attackTimer <= 0f && !cooling && player2.enemyDir.y < 1)
        {
            transform.localPosition = attack;
            attackTimer = maxAttackT;
            cooldown = maxCooldown;
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) && attackTimer <= 0f && !cooling && player2.enemyDir.y > 1)
        {
            transform.localPosition = attackUP;
            attackTimer = maxAttackT;
            cooldown = maxCooldown;
        }
    }

    void Timers()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else if (attackTimer < 0)
        {
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

        if (knocked && knockTimer > 0)
        {
            knockTimer -= Time.deltaTime;
        }
        if (knockTimer <= 0)
        {
            player1.enabled = true;
            knocked = false;
        }

        if (shakeDuration > 0 && knockShake == true)
        {
            cam.shakingMuch = shakeAmount;
            shakeDuration -= Time.deltaTime;
        }
        if (shakeDuration <= 0)
        {
            knockShake = false;
            shakeDuration = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            player1 = collision.GetComponent<Player1>();

            player1.enabled = false;
            player1.rb.velocity = player1.enemyDir.normalized * -35;
            player1.playerHealth -= player1.fistDamage;

            knockTimer = maxKnockT;
            knocked = true;

            shakeDuration = maxShakeDuration;
            knockShake = true;
        }
    }
}
