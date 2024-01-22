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

    TigerStyle tigerS;
    staffAttack staffA;
    Gun gunA;

    public GameMaster master;
    public TMP_Text roundsText;

    bool p1Select;
    bool p2Select;

    bool p1Locked;
    bool p2Locked;

    bool sStaff1;
    bool sStaff2;
    bool sGun1;
    bool sGun2;
    bool sTiger1;
    bool sTiger2;

    public ColorBlock thecolor;

    public Button[] buttons;
    public Players[] bothPlayers;
    public Fists[] bothFists;
    public staffAttack[] staffs;
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

    private void Update()
    {
        roundsText.text = "Rounds <br>" + master.maxRounds;
    }

    public void MoreRounds()
    {
        if (master.maxRounds < 100)
        {
            master.maxRounds++;
        }
    }

    public void LessRounds()
    {
        if(master.maxRounds > 1)
        {
            master.maxRounds--;
        }
    }

    public void GameOn()
    {
        if(p1Locked && p2Locked)
        {
            PlayersOn();

            platforms.SetActive(true);
            bars.SetActive(true);
            gameMenu.SetActive(false);

            if (sStaff1)
            {
                staffs[0] = bothPlayers[0].GetComponentInChildren<staffAttack>();
                staffs[0].enabled = true;
            }
            if (sStaff2)
            {
                staffs[1] = bothPlayers[1].GetComponentInChildren<staffAttack>();
                staffs[1].enabled = true;
            }

            if (sGun1)
            {
                guns[0] = bothPlayers[0].GetComponentInChildren<Gun>();
                guns[0].enabled = true;
                guns[0].sprite.SetActive(false);
            }
            if (sGun2)
            {
                guns[1] = bothPlayers[1].GetComponentInChildren<Gun>();
                guns[1].enabled = true;
                guns[1].sprite.SetActive(false);
            }

            if (sTiger1)
            {
                tiger[0] = bothPlayers[0].GetComponentInChildren<TigerStyle>();
                tiger[0].enabled = true;
                tiger[0].sprites.SetActive(false);
            }
            if (sTiger2)
            {
                tiger[1] = bothPlayers[1].GetComponentInChildren<TigerStyle>();
                tiger[1].enabled = true;
                tiger[1].sprites.SetActive(false);
            }
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
            sStaff1 = true;
            Instantiate(staff, bothPlayers[0].transform);

            staffA = bothPlayers[0].GetComponentInChildren<staffAttack>();
            staffA.enabled = false;

            p1Select = true;
        }

        if (p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            sStaff2 = true;
            Instantiate(staff, bothPlayers[1].transform);

            staffA = bothPlayers[1].GetComponentInChildren<staffAttack>();
            staffA.enabled = false;

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

            gunA = bothPlayers[0].GetComponentInChildren<Gun>();
            gunA.enabled = false;

            p1Select = true;
        }
        if(p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            sGun2 = true;
            Instantiate(gun, bothPlayers[1].transform);

            gunA = bothPlayers[1].GetComponentInChildren<Gun>();
            gunA.enabled = false;

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

            tigerS = bothPlayers[0].GetComponentInChildren<TigerStyle>();
            tigerS.enabled = false;

            p1Select = true;
        }
        if (p1Locked && !p2Locked)
        {
            lockInText.text = player2LockTxt;

            DeselectWeapon();

            p2Select = false;
            sTiger2 = true;
            Instantiate(tigerStyle, bothPlayers[1].transform);

            tigerS = bothPlayers[1].GetComponentInChildren<TigerStyle>();
            tigerS.enabled = false;

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
                    sStaff1 = false;
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
                    sStaff1 = false;
                    sGun2 = false;
                    sTiger2 = false;
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void WeaponLock()
    {
        if (!p1Locked)
        {
            lockInText.text = lockedInText;
            p1Locked = true;

            Player2Colors();

            foreach(Button button in buttons)
            {
                button.colors = thecolor;
            }
        }

        else if (p1Locked)
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
