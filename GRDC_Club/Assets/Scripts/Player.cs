using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Use this for initialization

    public Vector2 DeltaMove;

    public GameObject PlayerObject;

    private float x;
    private float y;

	public float speed = 1.5F;
 	public float spacing = 1.0F;
	private Vector3 pos;
	
    void Start()
    {
		pos = PlayerObject.transform.position;

        x = PlayerObject.transform.position.x;
        y = PlayerObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
            pos.y += spacing;
        if (Input.GetKeyDown(KeyCode.S))
            pos.y -= spacing;
        if (Input.GetKeyDown(KeyCode.A))
            pos.x -= spacing;
        if (Input.GetKeyDown(KeyCode.D))
            pos.x += spacing;

        PlayerObject.transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

        if (PlayerObject.transform.position.x != x)
        {
            DeltaMove.x = PlayerObject.transform.position.x - x;
        }
        if (PlayerObject.transform.position.y != y)
        {
            DeltaMove.y = PlayerObject.transform.position.y - y;
        }
    }
}
