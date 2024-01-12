using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameMenu;
    public GameObject bars;
    public GameObject platforms;

    public GameObject staff;
    public GameObject gun;
    public GameObject tigerStyle;
    public TMP_Text lockInText;

    bool p1Select;
    bool p2Select;
    bool p1Locked;
    bool p2Locked;

    bool sGun1;
    bool sGun2;
    bool sTiger1;
    bool sTiger2;

    public ColorBlock thecolor;

    public Button[] buttons;
    public Players[] bothPlayers;
    public Fists[] bothFists;
    public Gun[] guns;
    public TigerStyle[] tiger;

    string weaponString = "Weapon";
    string lockedInText = "Locked in!";
    string player2LockTxt = "Player 2 lock in?";

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.colors = thecolor;
        }
    }

    public void GameOn()
    {
        PlayersOn();

        platforms.SetActive(true);
        bars.SetActive(true);
        gameMenu.SetActive(false);

        if (sGun1)
        {
            guns[0] = bothPlayers[0].GetComponentInChildren<Gun>();
            guns[0].sprite.SetActive(false);
        }

        if (sGun2)
        {
            guns[1] = bothPlayers[1].GetComponentInChildren<Gun>();
            guns[1].sprite.SetActive(false);
        }

        if (sTiger1)
        {
            tiger[0] = bothPlayers[0].GetComponentInChildren<TigerStyle>();
            tiger[0].sprites.SetActive(false);
        }

        if (sTiger2)
        {
            tiger[1] = bothPlayers[1].GetComponentInChildren<TigerStyle>();
            tiger[1].sprites.SetActive(false);
        }

    }

    void PlayersOn()
    {
        /*player1M = player1.GetComponent<Players>();
        player2M = player2.GetComponent<Players>();*/

        foreach(Players player in bothPlayers)
        {
            player.enabled = true;
        }

        foreach(Fists fist in bothFists)
        {
            fist.enabled = true;
        }
    }

    public void Staff()
    {
        if (!p1Locked)
        {
            DeselectWeapon();

            p1Select = false;
            Instantiate(staff, bothPlayers[0].transform);
            p1Select = true;
        }

        if (p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            Instantiate(staff, bothPlayers[1].transform);
            p2Select = true;
        }
    }

    public void Gun()
    {
        if(!p1Locked)
        {
            DeselectWeapon();

            p1Select = false;
            sGun1 = true;
            Instantiate(gun, bothPlayers[0].transform);
            p1Select= true;
        }
        if(p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            sGun2 = true;
            Instantiate(gun, bothPlayers[1].transform);
            p2Select = true;
        }
    }

    public void Tiger()
    {
        if (!p1Locked)
        {
            DeselectWeapon();

            p1Select = false;
            sTiger1 = true;
            Instantiate(tigerStyle, bothPlayers[0].transform);
            p1Select = true;
        }
        if (p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            sTiger2 = true;
            Instantiate(tigerStyle, bothPlayers[1].transform);
            p2Select = true;
        }
    }

    public void DeselectWeapon()
    {
        if (!p1Locked)
        {
            foreach (Transform child in bothPlayers[0].transform)
            {
                if (child.tag == weaponString)
                {
                    sGun1 = false;
                    sTiger1 = false;
                    Destroy(child.gameObject);
                }
                    
            }
        }

        if(p1Locked && !p2Locked)
        {
            foreach (Transform child in bothPlayers[1].transform)
            {
                if (child.tag == weaponString)
                {
                    sGun2 = false;
                    sTiger2 = false;
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void WeaponLock()
    {
        if (p1Select && !p1Locked)
        {
            lockInText.text = lockedInText;
            p1Locked = true;

            Player2Colors();

            foreach(Button button in buttons)
            {
                button.colors = thecolor;
            }
        }

        if (p2Select && p1Locked)
        {
            p2Locked = true;
            lockInText.text = lockedInText;
        }
    }

    void Player2Colors()
    {
        Color darkBlue = new Color(0, 0f, 0.6f);

        thecolor.normalColor = Color.black;
        thecolor.highlightedColor = darkBlue;
        thecolor.pressedColor = Color.blue;
        thecolor.selectedColor = Color.blue;
        thecolor.disabledColor = Color.black;

        thecolor.colorMultiplier = 1;
        thecolor.fadeDuration = 0.25f;
    }
}
