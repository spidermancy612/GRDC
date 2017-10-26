using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MoveSelect : MonoBehaviour
{
    bool _finalSelection, _waitingForMoves;

    public GameObject MoveDisplay;

    public List<GameObject> MoveVectors;

    public GameObject ConfirmButton;

    private int Allowed, Index;

    private Dictionary<MoveVector, int> Moves;

    // Use this for initialization
    void Start()
    {
        MoveDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_waitingForMoves) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (Allowed > 0)
                {
                    for (int i = 0; i < MoveVectors.Count; i++)
                    {
                        if (hit.collider.gameObject.Equals(MoveVectors[i]))
                        {
                            //add one to the dictionary
                            //remove one from allowed
                            Allowed--;
                            var temp = (MoveVector) i;
                            int j;
                            if (Moves.TryGetValue(temp, out j))
                            {
                                Moves.Remove(temp);
                                Moves.Add(temp, j + 1);
                            }
                            else
                                Moves.Add(temp, 1);
                        }
                    }
                }
                if (Allowed == 0 && hit.collider.gameObject.Equals(ConfirmButton))
                {
                    _finalSelection = true;
                }
            }
        }
        if (Allowed == 0)
        {
            ConfirmButton.SetActive(true);
        }
        else
        {
            ConfirmButton.SetActive(false);
        }
        if (_finalSelection)
        {
            var cardHolder = GetComponentInParent<CardHolderLogic>();
            cardHolder.TurnSelection[Index].moveVector = Moves;
            MoveDisplay.SetActive(false);
            _waitingForMoves = false;
            cardHolder.WaitForMove = false;
        }
    }


    public void GetMove(int index, int allowable, float x, float y)
    {
        Moves = new Dictionary<MoveVector, int>();
        transform.SetPositionAndRotation(new Vector3(x, y, transform.position.z), transform.rotation);
        MoveDisplay.SetActive(true);
        Index = index;
        Allowed = allowable;
        _waitingForMoves = true;
        _finalSelection = false;
    }
}