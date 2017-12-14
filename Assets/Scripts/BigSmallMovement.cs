using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallMovement : MonoBehaviour 
{
    public float Speed = 5f;
    public bool Move = true;

    private Vector2 movementVector;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
    	movementVector = Vector2.zero;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!Move)
        {
            return;
        }
        
    	var horizontal = Input.GetAxis("Horizontal");
    	var vertical = Input.GetAxis("Vertical");

    	movementVector = new Vector2(horizontal, vertical);
        rigidBody.velocity = movementVector * Speed;
    }
}
