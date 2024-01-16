using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float attackTimer;
    public float maxAttackT;

    public float cooldown;
    public float maxCooldown;

    public float shake;

    bool cooling;
    bool attacking;

    Vector2 attack;
    Vector2 attackUP;

    Vector3 upAttackRot;
    Vector3 lowAttackRot;

    Players thisPlayer;

    public GameObject sprite;
    public GameObject bullet;

    Bullet bulletScr;

    //hand color
    SpriteRenderer playerSprite;
    public SpriteRenderer gunSprite;
    public GameObject hand;

    Camera camera;
    public CameraFollow cam;

    string player1 = "Player 1";
    string player2 = "Player 2";

    private void Start()
    {
        attackTimer = 0;
        attack = new Vector2(1f, 0.4f);
        attackUP = new Vector2(1f, 1f);

        upAttackRot = new Vector3(0, 0, 30);
        lowAttackRot = new Vector3(0, 0, 0);

        camera = FindAnyObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

        thisPlayer = GetComponentInParent<Players>();

        gunSprite = hand.GetComponent<SpriteRenderer>();
        playerSprite = GetComponentInParent<SpriteRenderer>();
        gunSprite.color = playerSprite.color;

        cooldown = maxCooldown;
    }

    private void Update()
    {
        Attack();
        Timers();
    }

    void Attack()
    {
        //player1 controls
        if (thisPlayer.CompareTag(player1))
        {
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                transform.localPosition = attack;
                transform.eulerAngles = lowAttackRot;

                AttackSet();
                bulletScr.bulletDirY = Vector2.zero;

            }
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                if (thisPlayer.facingRight)
                {
                    transform.localPosition = attackUP;
                    transform.eulerAngles = upAttackRot;

                    AttackSet();
                    bulletScr.bulletDirY = Vector2.up;
                }
                else
                {
                    transform.localPosition = attackUP;
                    transform.eulerAngles = -upAttackRot;

                    AttackSet();
                    bulletScr.bulletDirY = Vector2.up;
                }
            }
        }

        //player2 controls
        if (thisPlayer.CompareTag(player2))
        {
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                transform.localPosition = attack;
                transform.eulerAngles = lowAttackRot;

                AttackSet();
                bulletScr.bulletDirY = Vector2.zero;
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                if (thisPlayer.facingRight)
                {
                    transform.localPosition = attackUP;
                    transform.eulerAngles = upAttackRot;

                    AttackSet();
                    bulletScr.bulletDirY = Vector2.up;
                }
                else
                {
                    transform.localPosition = attackUP;
                    transform.eulerAngles = -upAttackRot;

                    AttackSet();
                    bulletScr.bulletDirY = Vector2.up;
                }
            }
        }
    }

    void AttackSet()
    {
        Instantiate(bullet, transform);

        bulletScr = GetComponentInChildren<Bullet>();

        bulletScr.bulletSpeed *= thisPlayer.enemyDir.normalized.x;

        bulletScr.transform.parent = null;

        sprite.SetActive(true);

        attackTimer = maxAttackT;
        attacking = true;

        cooldown = maxCooldown;
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
            sprite.SetActive(false);
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
    }
}
