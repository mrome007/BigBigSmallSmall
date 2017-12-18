using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallMovement : MonoBehaviour 
{
    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    private bool move = true;

    [SerializeField]
    private CameraUtility camUtility;

    private Vector2 movementVector;
    private Rigidbody2D rigidBody;
    private BigSmallSpriteChange dirSpriteChange;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
    	movementVector = Vector2.zero;
        rigidBody = GetComponent<Rigidbody2D>();
        dirSpriteChange = GetComponent<BigSmallSpriteChange>();
        Direction = Vector2.right;

        camUtility.CameraMoved += CameraMovedHandler;
        camUtility.CameraStopped += CameraStoppedHandler;
    }

    private void OnDestroy()
    {
        camUtility.CameraMoved -= CameraMovedHandler;
        camUtility.CameraStopped -= CameraStoppedHandler;
    }

    private void FixedUpdate()
    {
        if(!move)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }
        
    	var horizontal = Input.GetAxis("Horizontal");
    	var vertical = Input.GetAxis("Vertical");

    	movementVector = new Vector2(horizontal, vertical);
        rigidBody.velocity = movementVector * speed;
        ChangeDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void CameraStoppedHandler(object sender, EventArgs e)
    {
        move = true;
    }

    private void CameraMovedHandler(object sender, EventArgs e)
    {
        move = false;
    }

    private void ChangeDirection(float horizontal, float vertical)
    {
        if(horizontal != 0f || vertical != 0)
        {
            var isHorizontal = horizontal != 0f;
            var right = horizontal > 0f;
            var up = vertical > 0f;
            dirSpriteChange.ChangeSprite(isHorizontal, isHorizontal ? right : up);

            Direction = isHorizontal ? (right ? Vector2.right : Vector2.left) : (up ? Vector2.up : Vector2.down);
        }
    }
}
