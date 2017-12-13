using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallMovement : MonoBehaviour 
{
    public float Speed = 5f;
    public Transform TopRightCorner;
    public Transform BottomLeftCorner;

    private Vector2 movementVector;
    private float xMax;
    private float xMin;
    private float yMax;
    private float yMin;

    private void Start()
    {
    	movementVector = Vector2.zero;
        xMax = TopRightCorner.position.x;
        xMin = BottomLeftCorner.position.x;
        yMax = TopRightCorner.position.y;
        yMin = BottomLeftCorner.position.y;
    }

    private void Update()
    {
    	var horizontal = Input.GetAxis("Horizontal");
    	var vertical = Input.GetAxis("Vertical");

    	movementVector = new Vector2(horizontal, vertical);
    	transform.Translate(movementVector * Speed * Time.deltaTime);

    	CapMovement();
    }

    private void CapMovement()
    {
        var xPos = transform.position.x; 
    	xPos = Mathf.Clamp(xPos, xMin, xMax);
    	var yPos = transform.position.y; 
    	yPos = Mathf.Clamp(yPos, yMin, yMax);

    	transform.position = new Vector2(xPos, yPos);
    }
}
