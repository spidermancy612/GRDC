using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardHolderLogic : MonoBehaviour
{


    //public List<Card> Cards;

    public List<GameObject> Cards;

    public float MoveOdd, WeakMoveOdd, StrongMoveOdd, AttackOdd, ShieldOdd;

    private System.Random random;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawCards()
    {
        foreach (var card in Cards)
        {
            //Roll the card
            double result = random.NextDouble() * (MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd + ShieldOdd);

            if (result <= MoveOdd)
            {
                //MoveOdd
            }
            else if (result <= MoveOdd + WeakMoveOdd)
            {
                //WeakMoveOdd
            }
            else if (result <= MoveOdd + WeakMoveOdd + StrongMoveOdd)
            {
                //Strong Move Odd
            }
            else if (result <= MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd)
            {
                //AttackOdd
            }
            else /*if (result <= MoveOdd+WeakMoveOdd+StrongMoveOdd+AttackOdd+ShieldOdd)*/
            {
                //ShieldOdd
            }
            card.SetActive(true);
        }
    }
    public void EraseCards()
    {
        foreach (var card in Cards)
        {
            card.SetActive(false); ;
        }
    }
}
