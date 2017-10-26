using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderLogic : MonoBehaviour
{
    public Player player;
    public MoveSelect moveSelect;
    public List<GameObject> Cards; //List of visible cards
    public List<Material> CardMaterials; //Materials to be used on the cards
    public float MoveOdd, WeakMoveOdd, StrongMoveOdd, AttackOdd, ShieldOdd; //Odds of a card being drawn
    public List<TurnType> CardResults; //List of Card results with range 0-4
    private bool CardsIsVisible; //Whether or not the cards are currenly visible
    private int SelectedCards, CardsLeft;

    public bool WaitForMove;

    public List<Action> TurnSelection;

    // Use this for initialization
    void Start()
    {
        CardResults = new List<TurnType> { 0, 0, 0, 0, 0, 0, 0 };
        EraseCards();
        SelectedCards = 0;
        CardsLeft = 7;
        TurnSelection = new List<Action>();
        WaitForMove = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (CardsIsVisible && Input.GetMouseButtonDown(0) && SelectedCards < 3 && WaitForMove == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                //TODO Tell card it was clicked
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].Equals(hit.collider.gameObject) && CardResults[i] != TurnType.None)
                    {
                        TurnSelection.Add(new Action(CardResults[i]));

                        if (CardResults[i] == TurnType.Move || CardResults[i] == TurnType.WeakMove || CardResults[i] == TurnType.StrongMove)
                        {
                            //StartCoroutine(SelectMovementType(this, SelectedCards)); <-- gets intended movement then reports back (gives gameobject and the index of turns)
                            //WaitForMove is the number of moves it is waiting for, incase order gets weird
                            moveSelect.GetMove(TurnSelection.Count-1, 3, Cards[i].transform.position.x, Cards[i].transform.position.y);
                            WaitForMove = true;
                        }
                        //Set it so it wont draw next turn
                        CardResults[i] = TurnType.None;
                        SelectedCards++;
                        CardsLeft -= 1;
                    }
                }
            }
        }
        if ((SelectedCards == 3 || CardsLeft == 0) && WaitForMove == false)
        {
            SelectedCards = 0;
            player.Ready = true;
            EraseCards();
        }

    }

    /// <summary>
    /// Deal Cards
    /// Determines what cards are drawn and stores in CardResults
    /// </summary>
    public void DealCards()
    {
        for (int i = 0; i < CardResults.Count; i++)
        {
            //Roll the card
            double result = Random.value * (MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd + ShieldOdd);
            if (result <= MoveOdd)
            {
                CardResults[i] = TurnType.Move;
                //MoveOdd
            }
            else if (result <= MoveOdd + WeakMoveOdd)
            {
                CardResults[i] = TurnType.WeakMove;
                //WeakMoveOdd
            }
            else if (result <= MoveOdd + WeakMoveOdd + StrongMoveOdd)
            {
                CardResults[i] = TurnType.StrongMove;
                //Strong Move Odd
            }
            else if (result <= MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd)
            {
                CardResults[i] = TurnType.Attack;
                //AttackOdd
            }
            else /*if (result <= MoveOdd+WeakMoveOdd+StrongMoveOdd+AttackOdd+ShieldOdd)*/
            {
                CardResults[i] = TurnType.Shield;
                //ShieldOdd
            }
        }
        SelectedCards = 0;
        CardsLeft = 7;
    }

    /// <summary>
    /// Draw Cards
    /// Draws cards to screen using CardResults to determine what type it is
    /// If CardResults for the card is -1 it is not drawn to screen
    /// </summary>
    public void DrawCards()
    {
        for (int i = 0; i < CardResults.Count; i++)
        {
            if (CardResults[i] != TurnType.None)
            {
                MeshRenderer cardRenderer = Cards[i].GetComponent<MeshRenderer>();
                if (cardRenderer != null)
                {
                    cardRenderer.material = CardMaterials[(int)CardResults[i]];
                    //TODO Tell card what kind it is
                }
                CardController cardController = Cards[i].GetComponent<CardController>();
                cardController.DrawCard();
                Cards[i].SetActive(true);
            }
        }
        CardsIsVisible = true;
    }

    /// <summary>
    /// Erase Cards
    /// Remove Cards from screen
    /// </summary>
    public void EraseCards()
    {
        foreach (var card in Cards)
        {
            card.SetActive(false);
            //Reset the card to the original position and rotation so animations look right
            CardController cardController = card.GetComponent<CardController>();
            cardController.Reset();
        }
        //Set so we know the cards aren't on screen
        CardsIsVisible = false;
    }

    /// <summary>
    /// Returne the turns card selection
    /// </summary>
    /// <returns>List of the Turn Selection</returns>
    public List<Action> GetCardResult()
    {
        List<Action> temp = TurnSelection;
        TurnSelection = new List<Action>();
        return temp;
    }
}
