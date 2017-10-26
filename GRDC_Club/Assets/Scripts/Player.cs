using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Use this for initialization
    public Vector2 DeltaMove;
    public CardHolderLogic Cards;
    public bool Ready;
    public bool TurnPlaying;
    public GameObject Shield;
    private float x, y;
    public float speed = 1.5F;
    public float spacing = 1.0F;
    private Vector3 pos;

    private bool lastE, lastR;

    /// <summary>
    /// Initialize variables
    /// </summary>
    void Start()
    {
        pos = this.transform.position;

        x = this.transform.position.x;
        y = this.transform.position.y;

        lastE = false;
        lastR = false;

        TurnPlaying = false;
    }

    /// <summary>
    /// Get player input and play out turn
    /// </summary>
    void Update()
    {
        if (TurnPlaying == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
                pos.y += spacing;
            if (Input.GetKeyDown(KeyCode.S))
                pos.y -= spacing;
            if (Input.GetKeyDown(KeyCode.A))
                pos.x -= spacing;
            if (Input.GetKeyDown(KeyCode.D))
                pos.x += spacing;

            this.transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

            if (this.transform.position.x != x)
            {
                DeltaMove.x = this.transform.position.x - x;
            }
            if (this.transform.position.y != y)
            {
                DeltaMove.y = this.transform.position.y - y;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !lastR)
        {
            lastR = true;
            lastE = false;
            Cards.DrawCards();
        }
        if (Input.GetKeyDown(KeyCode.E) && !lastE)
        {
            lastE = true;
            lastR = false;
            Cards.EraseCards();
        }
    }
}
