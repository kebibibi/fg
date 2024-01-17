using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    BoxCollider2D bc2D;

    public Players playerEnemy;

    public float knockTimer;
    public float maxKnockT;
    public float bulletSpeed;
    public float missTimer;

    bool knocked;

    public Vector2 bulletDirY;

    Camera camera;
    public CameraFollow cam;
    SpriteRenderer sprite;

    string player1 = "Player 1";
    string player2 = "Player 2";

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();

        camera = FindFirstObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

        sprite = GetComponent<SpriteRenderer>();

        knockTimer = maxKnockT;
    }

    private void FixedUpdate()
    {
        rb2D.AddForce(Vector2.right * bulletSpeed + bulletDirY * 4, ForceMode2D.Impulse);
        Timers();
    }

    void Timers()
    {
        if (knockTimer > 0 && knocked)
        {
            knockTimer -= Time.deltaTime;
        }
        if (knockTimer <= 0)
        {
            knocked = false;
            playerEnemy.enabled = true;
            cam.shakingMuch = 0;

            Destroy(gameObject);
        }

        if(missTimer > 0)
        {
            missTimer -= Time.deltaTime;
        }
        if(missTimer < 0 && !knocked && knockTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player1) || collision.gameObject.CompareTag(player2))
        {
            playerEnemy = collision.GetComponent<Players>();

            playerEnemy.enabled = false;
            playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
            playerEnemy.playerHealth -= playerEnemy.abilityDamage;

            cam.shakingMuch = 0.35f;

            knocked = true;

            sprite.enabled = false;
            bc2D.enabled = false;
        }
    }
}
