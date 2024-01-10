using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    BoxCollider2D bc2D;

    public Players playerEnemy;

    float knockTimer;
    public float maxKnockT;
    public float bulletSpeed;
    public float missTimer;

    bool knocked;

    public Vector2 bulletDirY;

    Camera camera;
    public CameraFollow cam;
    SpriteRenderer sprite;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();

        camera = FindAnyObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb2D.AddForce(Vector2.right * bulletSpeed + bulletDirY * 4, ForceMode2D.Impulse);
        Timers();
    }

    void Timers()
    {
        if (knockTimer > 0)
        {
            knockTimer -= Time.deltaTime;
        }
        if (knockTimer <= 0)
        {
            playerEnemy.enabled = true;
            cam.shakingMuch = 0;

            Destroy(gameObject);
        }

        if(missTimer > 0)
        {
            missTimer -= Time.deltaTime;
        }
        if(missTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            playerEnemy = collision.GetComponent<Players>();
            playerEnemy = collision.GetComponentInParent<Players>(); 

            playerEnemy.enabled = false;
            playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
            playerEnemy.playerHealth -= playerEnemy.abilityDamage;

            cam.shakingMuch = 0.35f;

            knockTimer = maxKnockT;

            sprite.enabled = false;
            bc2D.enabled = false;
        }
    }
}
