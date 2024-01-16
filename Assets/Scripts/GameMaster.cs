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

    string winString = " wins";

    private void Start()
    {
        p1Start = bothPlayers[0].transform.position;
        p2Start = bothPlayers[1].transform.position;

        brTimer = maxbrTimer;
        winText.enabled = false;
    }

    void Update()
    {
        if(lastRound != round)
        {
            foreach (Players player in bothPlayers)
            {
                fist = player.GetComponentInChildren<Fists>();
                fist.enabled = false;

                player.enabled = false;
            }

            betweenR = true;
        }

        if(round == maxRounds)
        {
            gameEnd = true;
            GameEnd();
        }

        if (betweenR && brTimer > 0)
        {
            brTimer -= Time.deltaTime;
        }
        if(brTimer <= 0 && !gameEnd)
        {
            betweenR = false;
            NewRound();
            brTimer = maxbrTimer;
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
            winText.text = "Draw";
        }
        else
        {
            winText.text = bothPlayers[0].tag + winString;
        }
    }
}
