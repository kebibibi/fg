using System.Collections;
using System.Collections.Generic;
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

    public GameObject staff;
    public GameObject gun;

    bool p1Select;
    bool p2Select;

    public void GameOn()
    {
        PlayersOn();

        platforms.SetActive(true);
        bars.SetActive(true);
        gameMenu.SetActive(false);
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

    public void P1Staff()
    {
        if (!p1Select)
        {
            Instantiate(staff, player1.transform);
            p1Select = true;
        }
    }

    public void P2Staff()
    {
        if (!p2Select)
        {
            Instantiate(staff, player2.transform);
            p2Select = true;
        }
    }

    public void P1Gun()
    {
        if(!p1Select)
        {
            Instantiate(gun, player1.transform);
            p1Select= true;
        }
    }

    public void P2Gun()
    {
        if (!p2Select)
        {
            Instantiate(gun, player2.transform);
            p2Select = true;
        }
    }
}
