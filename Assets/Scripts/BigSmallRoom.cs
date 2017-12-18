﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallRoom : MonoBehaviour 
{
    public BigSmallRoom Up;
    public BigSmallRoom Down;
    public BigSmallRoom Left;
    public BigSmallRoom Right;

    public static Transform PlayerTransform;

    [SerializeField]
    private BigSmallRoomColliderController colliderController;

    [SerializeField]
    private Transform topRightTransform;

    [SerializeField]
    private Transform bottomLeftTransform;

    [SerializeField]
    private int numberOfHoles = 2;

    [SerializeField]
    private int numberOfButtons = 1;

    [SerializeField]
    private int numberOfEnemies = 5;

    private void Awake()
    {
        if(PlayerTransform == null)
        {
            PlayerTransform = GameObject.Find("BigSmallPlayer").transform;
        }
    }

    private void Start()
    {
        colliderController.EnableDoors(Up != null, Down != null, Left != null, Right != null);
        CreateRoomObstacles();
        CreateRoomEnemies();
    }

    private void CreateRoomObstacles()
    {
        var whichObstacle = Random.Range(0, 100) >= 50;
        if(whichObstacle)
        {
            var numberOfCreatedHoles = Random.Range(1, numberOfHoles + 1);
            for(int index = 0; index < numberOfCreatedHoles; index++)
            {
                var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
                var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
                var newPos = new Vector2(randomPosX, randomPosY);

                var hole = Instantiate(BigSmallRoomObjects.Instance.BigSmallHoleObject, newPos, Quaternion.identity);
                hole.transform.parent = transform;
            }
        }
        else
        {
            var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
            var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
            var newPos = new Vector2(randomPosX, randomPosY);
            var button = Instantiate(BigSmallRoomObjects.Instance.BigSmallButtonObject, newPos, Quaternion.identity);
            button.transform.parent = transform;
        }
    }

    private void CreateRoomEnemies()
    {
        var numberOfEnemiesCreated = Random.Range(1, numberOfEnemies);
        for (int index = 0; index < numberOfEnemiesCreated; index++)
        {
            var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
            var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
            var newPos = new Vector2(randomPosX, randomPosY);

            var enemy = Instantiate(BigSmallRoomObjects.Instance.BigSmallEnemyObjects[Random.Range(0, BigSmallRoomObjects.Instance.BigSmallEnemyObjects.Count)],
                            newPos, Quaternion.identity);
            enemy.transform.parent = transform;
        }
    }
}
