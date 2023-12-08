using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameMenu;
    public GameObject bars;

    public GameObject player1;
    public GameObject player2;

    public bool isPlaying;

    private void Start()
    {
        bars.SetActive(false);
        player1.SetActive(false);
        player2.SetActive(false);
        gameMenu.SetActive(false);
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
