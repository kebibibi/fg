using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    BoxCollider2D bc2D;

    Players playerEnemy;

    float knockTimer;
    public float maxKnockT;
    public float bulletSpeed;

    bool knocked;

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

    private void Update()
    {
        rb2D.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
        Timers();
    }

    void Timers()
    {
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

            cam.shakingMuch = 0.35f;

            knockTimer = maxKnockT;
            knocked = true;

            sprite.enabled = false;
            bc2D.enabled = false;
        }
    }
}
