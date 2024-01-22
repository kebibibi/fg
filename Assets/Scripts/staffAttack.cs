using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class staffAttack : MonoBehaviour
{
    public WeaponClass _weaponClass;

    float attackTimer;
    public float maxAttackT;
    public float cooldown;
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
    Vector3 upAttackRot;
    Vector3 lowAttackRot;

    Players playerEnemy;
    public Players thisPlayer;

    Camera camera;
    public CameraFollow cam;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    SpriteRenderer sprite;

    ParticleSystem damageCount;
    public TMP_Text damageText;

    AudioSource audios;
    public AudioClip[] abilityHitSounds;
    int randomAbHitClip;

    string player1 = "Player 1";
    string player2 = "Player 2";

    private void Start()
    {
        knockTimer = maxKnockT;
        attack = new Vector3(24f, 0.2f, 1);
        nAttack = new Vector3(2, 0.2f, 1);

        onBackRot = new Vector3(0, 0, 130);
        onAttackRot = new Vector3(0, 0, 0);

        upAttackRot = new Vector3(0, 0, 30);
        lowAttackRot = new Vector3(0, 0, 0);

        sprite = GetComponent<SpriteRenderer>();

        bc2D = GetComponent<BoxCollider2D>();
        bc2D.enabled = false;

        rb2D = GetComponentInParent<Rigidbody2D>();

        thisPlayer = GetComponentInParent<Players>();

        camera = FindFirstObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

        audios = GetComponent<AudioSource>();
        damageCount = GetComponent<ParticleSystem>();

        transform.eulerAngles = onBackRot;
        sprite.sortingOrder = -2;
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
        if (thisPlayer.CompareTag(player1))
        {
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                transform.eulerAngles = onAttackRot;
                sprite.sortingOrder = 1;

                attackTimer = maxAttackT;
                attacking = true;

                cooldown = maxCooldown;
                bc2D.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                if (thisPlayer.facingRight)
                {
                    transform.eulerAngles = upAttackRot;
                    sprite.sortingOrder = 1;

                    attackTimer = maxAttackT;
                    attacking = true;

                    cooldown = maxCooldown;
                    bc2D.enabled = true;
                }
                else
                {
                    transform.eulerAngles = -upAttackRot;
                    sprite.sortingOrder = 1;

                    attackTimer = maxAttackT;
                    attacking = true;

                    cooldown = maxCooldown;
                    bc2D.enabled = true;
                }
            }
        }

        //player2 controls
        if (thisPlayer.CompareTag(player2))
        {
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                transform.eulerAngles = onAttackRot;
                sprite.sortingOrder = 1;

                attackTimer = maxAttackT;
                attacking = true;

                cooldown = maxCooldown;
                bc2D.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                if (thisPlayer.facingRight)
                {
                    transform.eulerAngles = upAttackRot;
                    sprite.sortingOrder = 1;

                    attackTimer = maxAttackT;
                    attacking = true;

                    cooldown = maxCooldown;
                    bc2D.enabled = true;
                }
                else
                {
                    transform.eulerAngles = -upAttackRot;
                    sprite.sortingOrder = 1;

                    attackTimer = maxAttackT;
                    attacking = true;

                    cooldown = maxCooldown;
                    bc2D.enabled = true;
                }
            }
        }
    }

    void Timers()
    {
        if (attacking)
        {
            rb2D.isKinematic = true;
            transform.localScale = Vector2.MoveTowards(transform.localScale, attack, 3f);
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer <= 0)
        {
            rb2D.isKinematic = false;
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
            cooldown = 0;
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
        if (collision.gameObject.CompareTag(player1) || collision.gameObject.CompareTag(player2))
        {
            playerEnemy = collision.GetComponent<Players>();

            thisPlayer.damageText.text = _weaponClass.critDamage.ToString();

            playerEnemy.enabled = false;
            playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
            playerEnemy.playerHealth -= _weaponClass.critDamage;

            cam.shakingMuch = 0.35f;

            knockTimer = maxKnockT;
            knocked = true;

            RandomClip();

            audios.clip = abilityHitSounds[randomAbHitClip];
            audios.Play();

            damageCount.Play();
            playerEnemy.particles.Play(false);
        }
    }

    void RandomClip()
    {
        randomAbHitClip = Random.Range(0, 2);
    }
}
