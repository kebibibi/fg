using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    BoxCollider2D bc2D;

    Players thisPlayer;
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

    ParticleSystem damageCount;
    public TMP_Text damageText;

    AudioSource audios;
    public AudioClip[] abilityHitSounds;
    int randomAbHitClip;

    string player1 = "Player 1";
    string player2 = "Player 2";

    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        thisPlayer = GetComponentInParent<Players>();

        camera = FindFirstObjectByType<Camera>();
        cam = camera.GetComponent<CameraFollow>();

        sprite = GetComponent<SpriteRenderer>();

        audios = GetComponent<AudioSource>();
        damageCount = GetComponent<ParticleSystem>();

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

            thisPlayer.damageText.text = thisPlayer.abilityDamage.ToString();

            playerEnemy.enabled = false;
            playerEnemy.rb.velocity = playerEnemy.enemyDir.normalized * -35;
            playerEnemy.playerHealth -= thisPlayer.abilityDamage;

            cam.shakingMuch = 0.35f;

            knocked = true;

            sprite.enabled = false;
            bc2D.enabled = false;

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
