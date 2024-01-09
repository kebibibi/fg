using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerStyle : MonoBehaviour
{
    public Players player;

    SpriteRenderer tigerSprites;
    SpriteRenderer playerSprites;

    void Start()
    {
        player = GetComponentInParent<Players>();

        player.maxSpeed += 8;
        player.jumpingForce += 4;
        player.dashForce += 18;
        player.fistDamage += 5;

        tigerSprites = GetComponentInChildren<SpriteRenderer>();
        playerSprites = GetComponentInParent<SpriteRenderer>();
        tigerSprites.color = playerSprites.color;
    }
}
