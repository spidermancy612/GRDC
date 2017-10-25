using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderLogic : MonoBehaviour
{


    //public List<Card> Cards;

    public List<GameObject> Cards;

    public List<Material> CardMaterials;

    public float MoveOdd, WeakMoveOdd, StrongMoveOdd, AttackOdd, ShieldOdd;

    private double result;
    public List<int> CardResults;

    private bool CardsIsVisible;
    // Use this for initialization
    void Start()
    {
        CardResults = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
        EraseCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (CardsIsVisible && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            }
        }

    }

    public void DrawCards()
    {
        for (int i = 0; i < CardResults.Count; i++)
        {
            //Roll the card
            result = Random.value * (MoveOdd + WeakMoveOdd + StrongMoveOdd + AttackOdd + ShieldOdd);
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
            MeshRenderer cardRenderer = Cards[i].GetComponent<MeshRenderer>();
            if (cardRenderer != null)
            {
                cardRenderer.material = CardMaterials[CardResults[i]];
            }
            Cards[i].SetActive(true);
        }
        CardsIsVisible = true;
    }
    public void EraseCards()
    {
        foreach (var card in Cards)
        {
            card.SetActive(false);
            CardResults = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
        }
        CardsIsVisible = false;
    }
}
