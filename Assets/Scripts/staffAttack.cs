using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class staffAttack : MonoBehaviour
{
    float attackTimer;
    public float maxAttackT;
    float cooldown;
    public float maxCooldown;
    public float knockTimer;
    public float maxKnockT;

    bool cooling;
    bool knocked;
    bool attacking;

    Vector2 attack;
    Vector2 nAttack;

    Vector3 onBackRot;
    Vector3 onAttackRot;

    Players playerEnemy;
    public Players thisPlayer;

    Camera camera;
    public CameraFollow cam;

    BoxCollider2D bc2D;
    SpriteRenderer sprite;

    private void Start()
    {
        knockTimer = maxKnockT;
        attack = new Vector3(24f, 0.2f, 1);
        nAttack = new Vector3(2, 0.2f, 1);

        onBackRot = new Vector3(0, 0, 130);
        onAttackRot = new Vector3(0, 0, 0);

        sprite = GetComponent<SpriteRenderer>();

        bc2D = GetComponent<BoxCollider2D>();
        bc2D.enabled = false;

        thisPlayer = GetComponentInParent<Players>();

        camera = FindAnyObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

        transform.eulerAngles = onBackRot;
        sprite.sortingOrder = -1;
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
        if (thisPlayer.CompareTag("Player1"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling)
            {
                transform.eulerAngles = onAttackRot;
                sprite.sortingOrder = 1;

                attackTimer = maxAttackT;
                attacking = true;

                cooldown = maxCooldown;
                bc2D.enabled = true;
            }
        }

        //player2 controls
        if (thisPlayer.CompareTag("Player2"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling)
            {
                transform.eulerAngles = onAttackRot;
                sprite.sortingOrder = 1;

                attackTimer = maxAttackT;
                attacking = true;

                cooldown = maxCooldown;
                bc2D.enabled = true;
            }
        }
    }

    void Timers()
    {
        if (attacking)
        {
            transform.localScale = Vector2.MoveTowards(transform.localScale, attack, 3f);
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer <= 0)
        {
            attacking = false;
            transform.localScale = Vector2.MoveTowards(transform.localScale, nAttack, 4f);
            bc2D.enabled = false;
            cooling = true;
        }

        if (cooling && cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
            sprite.sortingOrder = -1;
            transform.eulerAngles = onBackRot;
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
            playerEnemy.enabled = true;
            cam.shakingMuch = 0;
            knocked = false;
            knockTimer = maxKnockT;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            playerEnemy = collision.GetComponent<Players>();

            playerEnemy.enabled = false;
            playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
            playerEnemy.playerHealth -= playerEnemy.abilityDamage;

            cam.shakingMuch = 0.35f;

            knockTimer = maxKnockT;
            knocked = true;
        }
    }
}
