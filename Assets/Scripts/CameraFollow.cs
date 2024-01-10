using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Vector2 shake;
    public float shakingMuch;

    public Vector2 middle;

    void FixedUpdate()
    {
        shake = Random.insideUnitSphere * shakingMuch;

        middle = Vector2.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        transform.position = new Vector3(middle.x + shake.x, 7 + shake.y, -10);
    }
}
