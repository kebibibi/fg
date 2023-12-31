using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float attackTimer;
    public float maxAttackT;
    float cooldown;
    public float maxCooldown;
    public float shake;

    bool cooling;
    bool attacking;

    Vector2 attack;
    Vector2 nAttack;
    Vector2 attackUP;

    Vector2 bulletStart;

    Vector3 upAttackRot;
    Vector3 lowAttackRot;

    Players thisPlayer;

    public GameObject sprite;
    public GameObject bullet;

    Bullet bulletScr;

    Camera camera;
    public CameraFollow cam;

    private void Start()
    {
        attackTimer = 0;
        attack = new Vector2(1f, 0.4f);
        attackUP = new Vector2(1f, 1f);

        upAttackRot = new Vector3(0, 0, 35);
        lowAttackRot = new Vector3(0, 0, 0);

        sprite.SetActive(false);

        camera = FindAnyObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

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
        if (thisPlayer.CompareTag("Player1"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                AttackSet();

                bulletScr.bulletDirY = Vector2.zero;

                transform.localPosition = attack;
                transform.eulerAngles = lowAttackRot;
            }
            if (Input.GetKeyDown(KeyCode.Q) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                AttackSet();

                bulletScr.bulletDirY = Vector2.up;

                transform.localPosition = attackUP;
                transform.eulerAngles = upAttackRot;
            }
        }

        //player2 controls
        if (thisPlayer.CompareTag("Player2"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y < 1)
            {
                AttackSet();

                bulletScr.bulletDirY = Vector2.zero;

                transform.localPosition = attack;
                transform.eulerAngles = lowAttackRot;
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) && attackTimer <= 0f && !cooling && thisPlayer.enemyDir.y > 1)
            {
                AttackSet();

                bulletScr.bulletDirY = Vector2.up;

                transform.localPosition = attackUP;
                transform.eulerAngles = thisPlayer.transform.localScale.x * upAttackRot;
            }
        }
    }

    void AttackSet()
    {
        Instantiate(bullet, this.gameObject.transform.position, this.gameObject.transform.rotation, this.transform);

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
