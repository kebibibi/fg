using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TigerStyle : MonoBehaviour
{
    Players thisPlayer;
    SpriteRenderer playerSprite;

    public GameObject sprites;

    public bool Mode;
    public float ModeTimer;
    public float maxModeT;

    public float modeSpeed;
    public float modeDash;
    public float modeDamage;
    public float modeJump;

    public SpriteRenderer[] sprite;

    private void Start()
    {
        thisPlayer = GetComponentInParent<Players>();

        playerSprite = GetComponentInParent<SpriteRenderer>();

        foreach(SpriteRenderer sprite in sprite)
        {
            sprite.color = playerSprite.color;
        }
    }

    void Update()
    {
        Attack();

        Timer();
    }

    void Attack()
    {
        if (thisPlayer.CompareTag("Player1"))
        {
            if (Input.GetKey(KeyCode.Q) && !Mode)
            {
                ModeOn();
                Mode = true;
                ModeTimer = maxModeT;
            }
        }

        if (thisPlayer.CompareTag("Player2"))
        {
            if (Input.GetKey(KeyCode.Keypad9) && !Mode)
            {
                ModeOn();
                Mode = true;
                ModeTimer = maxModeT;
            }
        }
    }

    void Timer()
    {
        if(ModeTimer > 0 && Mode)
        {
            ModeTimer -= Time.deltaTime;
        }

        if (ModeTimer < 0 && Mode)
        {
            sprites.SetActive(false);
            ModeOff();
        }
    }

    void ModeOn()
    {
        sprites.SetActive(true);

        thisPlayer.maxSpeed += modeSpeed;
        thisPlayer.maxDashForce += modeDash;
        thisPlayer.fistDamage += modeDamage;
        thisPlayer.jumpingForce += modeJump;
    }

    void ModeOff()
    {
        thisPlayer.maxSpeed -= modeSpeed;
        thisPlayer.maxDashForce -= modeDash;
        thisPlayer.fistDamage -= modeDamage;
        thisPlayer.jumpingForce -= modeJump;
        Mode = false;
    }
}
