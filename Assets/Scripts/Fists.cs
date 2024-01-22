using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fists : MonoBehaviour
{
    public WeaponClass _weaponClass;

    BoxCollider2D bc2D;

    //timers
    float attackTimer;
    public float maxAttackT;
    float cooldown;
    public float maxCooldown;
    float knockTimer;
    public float maxKnockT;

    //statuses
    bool cooling;
    bool knocked;
    bool attacking;
    bool hurt;

    //fist positions
    Vector2 attack;
    Vector2 nAttack;
    Vector2 attackUP;

    //players
    Players playerEnemy;
    Players thisPlayer;

    //camera
    public CameraFollow cam;

    //damage count text
    ParticleSystem damageCount;

    //audios
    AudioSource audios;
    public AudioClip[] hitSounds;
    public AudioClip[] dashHitSounds;
    int randomHitClip;
    int randomDHitClip;

    //texts
    string player1 = "Player 1";
    string player2 = "Player 2";

    private void OnEnable()
    {
        attackTimer = 0;
        knockTimer = maxKnockT;

        attack = new Vector2(1.25f, 0.25f);
        attackUP = new Vector2(1.25f, 1f);
        nAttack = new Vector2(0.3f, 0.25f);

        thisPlayer = GetComponentInParent<Players>();
        damageCount = GetComponent<ParticleSystem>();
        audios = GetComponent<AudioSource>();
        bc2D = GetComponent<BoxCollider2D>();
        bc2D.enabled = false;

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
            if (Input.GetKeyDown(KeyCode.E) && attackTimer <= 0f && !cooling)
            {
                if(thisPlayer.enemyDir.y < 1)
                {
                    bc2D.enabled = true;
                    transform.localPosition = attack;
                    attackTimer = maxAttackT;
                    attacking = true;
                    cooldown = maxCooldown;
                }
                else if(thisPlayer.enemyDir.y > 1)
                {
                    bc2D.enabled = true;
                    transform.localPosition = attackUP;
                    attackTimer = maxAttackT;
                    attacking = true;
                    cooldown = maxCooldown;
                }
            }
        }

        //player2 controls
        if (this.gameObject.CompareTag("Fist2"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad7) && attackTimer <= 0f && !cooling)
            {
                if (thisPlayer.enemyDir.y < 1)
                {
                    bc2D.enabled = true;
                    transform.localPosition = attack;
                    attackTimer = maxAttackT;
                    attacking = true;
                    cooldown = maxCooldown;
                }
                else if (thisPlayer.enemyDir.y > 1)
                {
                    bc2D.enabled = true;
                    transform.localPosition = attackUP;
                    attackTimer = maxAttackT;
                    attacking = true;
                    cooldown = maxCooldown;
                }
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
            bc2D.enabled = false;
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
            hurt = false;
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
            if (!collision.gameObject.transform.parent)
            {
                hurt = true;
            }
            else
            {
                hurt = false;
            }
            
            if (hurt && !thisPlayer.dashing)
            {
                playerEnemy = collision.GetComponent<Players>();

                thisPlayer.damageText.text = _weaponClass.damage.ToString();

                playerEnemy.enabled = false;
                playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
                playerEnemy.playerHealth -= _weaponClass.damage;

                cam.shakingMuch = 0.2f;

                knockTimer = maxKnockT;
                knocked = true;

                RandomClip();

                audios.clip = hitSounds[randomHitClip];
                audios.Play();

                damageCount.Play();
            }

            if (hurt && thisPlayer.dashing)
            {
                playerEnemy = collision.GetComponent<Players>();

                thisPlayer.damageText.text = _weaponClass.critDamage.ToString();

                playerEnemy.enabled = false;
                playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
                playerEnemy.playerHealth -= _weaponClass.critDamage;

                cam.shakingMuch = 0.5f;

                knockTimer = maxKnockT;
                knocked = true;

                RandomClip();

                audios.clip = dashHitSounds[randomDHitClip];
                audios.Play();

                damageCount.Play();
                playerEnemy.particles.Play(false);
            }
        }
    }

    void RandomClip()
    {
        randomHitClip = Random.Range(0, 3);
        randomDHitClip = Random.Range(0, 2);
    }
}
