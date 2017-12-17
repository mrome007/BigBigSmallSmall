﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallProjectile : MonoBehaviour 
{
    public enum BigSmall
    {
        Big,
        Small
    }

    private float speed;
    private Vector2 direction;
    private BigSmall projectileType;
    private Vector3 initalPosition;

    public void Initialize(float scale, Vector2 dir, bool small)
    {
        direction = dir;
        projectileType = small ? BigSmall.Small : BigSmall.Big;
        var maxSpeed = 15f;
        speed = maxSpeed - (float)((int)scale / 2);
        initalPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        var distance = Vector3.SqrMagnitude(transform.position - initalPosition);
        if(distance > 400f)
        {
            Destroy(gameObject);
        }
    }
}
