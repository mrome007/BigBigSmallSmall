using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallMovement : MonoBehaviour 
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private bool move = true;

    [SerializeField]
    private CameraUtility camUtility;

    private Vector2 movementVector;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
    	movementVector = Vector2.zero;
        rigidBody = GetComponent<Rigidbody2D>();

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
    }

    private void CameraStoppedHandler(object sender, EventArgs e)
    {
        move = true;
    }

    private void CameraMovedHandler(object sender, EventArgs e)
    {
        move = false;
    }
}
