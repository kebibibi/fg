using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameMenu;
    public GameObject bars;
    public GameObject platforms;

    public GameObject player1;
    public GameObject player2;
    Players player1M;
    Players player2M;

    Fists player1F;
    Fists player2F;

    public bool isPlaying;

    private void Start()
    {
        PlayersOff();

        platforms.SetActive(false);
        bars.SetActive(false);
        gameMenu.SetActive(false);
    }

    void PlayersOff()
    {
        player1M = player1.GetComponent<Players>();
        player2M = player2.GetComponent<Players>();

        player1F = player1.GetComponentInChildren<Fists>();
        player2F = player2.GetComponentInChildren<Fists>();

        player1M.enabled = false;
        player2M.enabled = false;

        player1F.enabled = false;
        player2F.enabled = false;
    }

    public void GameMenuOn()
    {
        menu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
