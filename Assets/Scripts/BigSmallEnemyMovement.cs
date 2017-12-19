using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallEnemyMovement : MonoBehaviour 
{   
    public float Speed;
    private SpriteRenderer spriteRenderer;
    private float moveTimer = 2f;
    private Vector2 movementDirection;
    private bool move = true;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Speed = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if(!move)
        {
            return;
        }

        if(moveTimer >= 0)
        {
            movementDirection = (Vector2)BigSmallRoom.PlayerTransform.position - (Vector2)transform.position;
            transform.Translate(movementDirection.normalized * Speed * Time.deltaTime);
            spriteRenderer.flipX = movementDirection.x < 0;
        }
        else
        {
            move = false;
            Invoke("RestoreMovement", Random.Range(0.25f, 0.35f));
            moveTimer = Random.Range(3f, 5f);
        }

        moveTimer -= Time.deltaTime;
    }

    private void RestoreMovement()
    {
        move = true;
    }

    private void OnEnable()
    {
        move = true;
        moveTimer = 2f;
    }
}
