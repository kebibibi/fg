using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject menu;
    public GameObject gameMenu;
    public GameObject bars;
    public GameObject platforms;

    Players player1M;
    Players player2M;

    Fists player1F;
    Fists player2F;

    Gun gunSprite1;
    Gun gunSprite2;

    public GameObject staff;
    public GameObject gun;
    public TMP_Text lockInText;

    bool p1Select;
    bool p2Select;
    bool p1Locked;
    public bool p2Locked;

    string weaponString = "Weapon";
    string lockedInText = "Locked in!";
    string player2LockTxt = "Player 2 lock in?";

    public void GameOn()
    {
        PlayersOn();

        platforms.SetActive(true);
        bars.SetActive(true);
        gameMenu.SetActive(false);

        gunSprite1 = player1M.GetComponentInChildren<Gun>();
        gunSprite1.sprite.SetActive(false);

        gunSprite2 = player2M.GetComponentInChildren<Gun>();
        gunSprite2.sprite.SetActive(false);
    }

    void PlayersOn()
    {
        player1M = player1.GetComponent<Players>();
        player2M = player2.GetComponent<Players>();

        player1F = player1.GetComponentInChildren<Fists>();
        player2F = player2.GetComponentInChildren<Fists>();

        player1M.enabled = true;
        player2M.enabled = true;

        player1F.enabled = true;
        player2F.enabled = true;
    }

    public void Staff()
    {
        if (!p1Locked)
        {
            DeselectWeapon();

            p1Select = false;
            Instantiate(staff, player1.transform);
            p1Select = true;
        }

        if (p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            Instantiate(staff, player2.transform);
            p2Select = true;
        }
    }

    public void Gun()
    {
        if(!p1Locked)
        {
            DeselectWeapon();

            p1Select = false;
            Instantiate(gun, player1.transform);
            p1Select= true;
        }
        if(p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            Instantiate(gun, player2.transform);
            p2Select = true;
        }
    }

    public void DeselectWeapon()
    {
        if (!p1Locked)
        {
            foreach (Transform child in player1.transform)
            {
                if (child.tag == weaponString)
                    Destroy(child.gameObject);
            }
        }

        if(p1Locked && !p2Locked)
        {
            foreach (Transform child in player2.transform)
            {
                if (child.tag == weaponString)
                    Destroy(child.gameObject);
            }
        }
    }

    public void WeaponLock()
    {
        if (p1Select && !p1Locked)
        {
            lockInText.text = lockedInText;
            p1Locked = true;
        }

        if (p2Select && p1Locked)
        {
            p2Locked = true;
            lockInText.text = lockedInText;
        }
    }
}
