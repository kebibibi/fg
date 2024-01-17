using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    int lastRound;
    public int round;
    public int player1Rounds;
    public int player2Rounds;
    [Range(1, 100)]
    public int maxRounds;

    bool gameEnd;
    public TMP_Text winText;

    public float maxbrTimer;
    float brTimer;
    bool betweenR;

    public Players[] bothPlayers;
    Fists fist;

    public Vector2 p1Start;
    public Vector2 p2Start;

    string winString = " wins game";

    private void Start()
    {
        p1Start = bothPlayers[0].transform.position;
        p2Start = bothPlayers[1].transform.position;

        brTimer = maxbrTimer;
        winText.enabled = false;
    }

    void Update()
    {
        PlayerHealths();

        if (lastRound != round)
        {
            foreach (Players player in bothPlayers)
            {
                fist = player.GetComponentInChildren<Fists>();
                fist.enabled = false;

                player.enabled = false;
            }

            betweenR = true;
        }

        if (round == maxRounds)
        {
            gameEnd = true;
            GameEnd();
        }

        if (betweenR && brTimer > 0)
        {
            winText.enabled = true;
            brTimer -= Time.deltaTime;
        }
        if(brTimer <= 0 && !gameEnd)
        {
            winText.enabled = false;
            betweenR = false;
            NewRound();
            brTimer = maxbrTimer;
        }
    }

    void PlayerHealths()
    {
        if (bothPlayers[1].dead && bothPlayers[0].dead)
        {
            winText.text = "Round Draw";

            round++;
            player1Rounds++;
            player2Rounds++;

            bothPlayers[0].dead = false;
            bothPlayers[1].dead = false;
        }

        if (bothPlayers[0].dead && !bothPlayers[1].dead)
        {
            winText.text = "P2 Wins Round";

            round++;
            player2Rounds++;
            bothPlayers[0].dead = false;
        }
        if (bothPlayers[1].dead && !bothPlayers[0].dead)
        {
            winText.text = "P1 Wins Round";

            round++;
            player1Rounds++;
            bothPlayers[1].dead = false;
        }
    }

    void NewRound()
    {
        lastRound = round;

        bothPlayers[0].transform.position = p1Start;
        bothPlayers[1].transform.position = p2Start;

        foreach(Players player in bothPlayers)
        {
            fist = player.GetComponentInChildren<Fists>();
            fist.enabled = true;

            player.enabled = true;
            player.playerHealth = 100;
        }
    }

    void GameEnd()
    {
        winText.enabled = true;

        if (player1Rounds < player2Rounds)
        {
            winText.text = bothPlayers[1].tag + winString;
        }
        else if (player1Rounds == player2Rounds)
        {
            winText.text = "Game Draw";
        }
        else
        {
            winText.text = bothPlayers[0].tag + winString;
        }
    }
}
