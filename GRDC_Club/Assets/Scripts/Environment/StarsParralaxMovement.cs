using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsParralaxMovement : MonoBehaviour
{

    public GameObject Stars1;
    public GameObject Stars2;
    public GameObject Stars3;

    public List<Player> Players;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Move = new Vector2(0, 0);
        foreach (var player in Players)
        {
            Move += player.DeltaMove;
        }
        Move /= Players.Count;

        //MoveBackdrops
        Move /= 2;
        Stars3.transform.Translate(-Move.x * 0.25F, 0, -Move.y * 0.25F);

        Move /= 2;
        Stars2.transform.Translate(-Move.x * 0.25F, 0, -Move.y * 0.25F);

        Move /= 2;
        Stars1.transform.Translate(-Move.x * 0.25F, 0, -Move.y * 0.25F);
    }
}
