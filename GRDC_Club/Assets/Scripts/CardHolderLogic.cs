using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderLogic : MonoBehaviour
{
    public List<GameObject> Cards; //List of visible cards


    public List<Material> CardMaterials; //Materials to be used on the cards

    public float MoveOdd, WeakMoveOdd, StrongMoveOdd, AttackOdd, ShieldOdd; //Odds of a card being drawn
    public List<int> CardResults; //List of Card results with range 0-4

    private bool CardsIsVisible; //Whether or not the cards are currenly visible

    private int SelectedCards;

    // Use this for initialization
    void Start()
    {
        CardResults = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
        EraseCards();
        SelectedCards = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CardsIsVisible && Input.GetMouseButtonDown(0) && SelectedCards < 3)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //TODO Tell card it was clicked
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].Equals(hit.collider.gameObject))
                    {
                        //Set it so it wont draw nerst turn
                        CardResults[i] = -1;
                        SelectedCards++;
                    }
                }
            }
        }

    }

    /*
     * Draw Cards
     * Using Card draw odds, draws the cards from the deck and displays them on screen
     */
    public void DealCards()
    {
        for (int i = 0; i < CardResults.Count; i++)
        {
            //Roll the card
            double result = Random.value * (MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd + ShieldOdd);
            if (result <= MoveOdd)
            {
                CardResults[i] = 0;
                //MoveOdd
            }
            else if (result <= MoveOdd + WeakMoveOdd)
            {
                CardResults[i] = 1;
                //WeakMoveOdd
            }
            else if (result <= MoveOdd + WeakMoveOdd + StrongMoveOdd)
            {
                CardResults[i] = 2;
                //Strong Move Odd
            }
            else if (result <= MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd)
            {
                CardResults[i] = 3;
                //AttackOdd
            }
            else /*if (result <= MoveOdd+WeakMoveOdd+StrongMoveOdd+AttackOdd+ShieldOdd)*/
            {
                CardResults[i] = 4;
                //ShieldOdd
            }
        }
        SelectedCards = 0;
    }

    public void DrawCards()
    {
        for (int i = 0; i < CardResults.Count; i++)
        {
            if (CardResults[i] != -1)
            {
                //Tell the cards which material to use so they look like cards
                MeshRenderer cardRenderer = Cards[i].GetComponent<MeshRenderer>();
                if (cardRenderer != null)
                {
                    cardRenderer.material = CardMaterials[CardResults[i]];
                    //TODO Tell card what kind it is
                }

                //Tell the cards to come in all pretty like (animate)
                CardController cardController = Cards[i].GetComponent<CardController>();
                cardController.DrawCard();
                Cards[i].SetActive(true);
            }
        }
        //Set so we know the cards are on screen
        CardsIsVisible = true;
    }
    /*
    * Make the cards disapear
     */
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

    /*
    *   Get Card Results
    *   Returns the card result for the specified index so i can be acted out
    */
    public int GetCardResult(int card)
    {
        return CardResults[card];
    }
}
