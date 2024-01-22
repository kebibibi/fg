using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TigerStyle : MonoBehaviour
{
    public WeaponClass _weaponClass;

    Players thisPlayer;
    Fists _fists;
    SpriteRenderer playerSprite;

    public GameObject sprites;

    public bool mode;
    public float modeTimer;
    public float maxModeT;

    public float modeSpeed;
    public float modeDash;
    public float modeJump;

    public SpriteRenderer[] sprite;

    string player1 = "Player 1";
    string player2 = "Player 2";

    private void OnEnable()
    {
        thisPlayer = GetComponentInParent<Players>();

        _fists = thisPlayer.GetComponentInChildren<Fists>();

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
        if (thisPlayer.CompareTag(player1))
        {
            if (Input.GetKey(KeyCode.Q) && !mode)
            {
                ModeOn();
                mode = true;
                modeTimer = maxModeT;
            }
        }

        if (thisPlayer.CompareTag(player2))
        {
            if (Input.GetKey(KeyCode.Keypad9) && !mode)
            {
                ModeOn();
                mode = true;
                modeTimer = maxModeT;
            }
        }
    }

    void Timer()
    {
        if(modeTimer > 0 && mode)
        {
            modeTimer -= Time.deltaTime;
        }

        if (modeTimer < 0 && mode)
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
        _fists._weaponClass.damage += _weaponClass.damage;
        _fists._weaponClass.critDamage += _weaponClass.critDamage;
        thisPlayer.jumpingForce += modeJump;
    }

    void ModeOff()
    {
        thisPlayer.maxSpeed -= modeSpeed;
        thisPlayer.maxDashForce -= modeDash;
        _fists._weaponClass.damage -= _weaponClass.damage;
        _fists._weaponClass.critDamage -= _weaponClass.critDamage;
        thisPlayer.jumpingForce -= modeJump;
        mode = false;
    }
}
