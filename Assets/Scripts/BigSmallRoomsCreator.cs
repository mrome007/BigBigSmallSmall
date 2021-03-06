﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallRoomsCreator : MonoBehaviour 
{
    [SerializeField]
    private BigSmallRoom roomPrefab;

    [SerializeField]
    private BigSmallRoom winRoomPrefab;

    [SerializeField]
    private int numberOfColumns = 12;

    [SerializeField]
    private int numberOfRows = 10;

    private BigSmallRoom [,] roomsGrid;
    private Stack<Vector2Int> gridPositions;
    private float xPositionOffset = 17f;
    private float yPositionOffset = 11f;
    private GameObject roomsGridParentObject;

    private void Awake()
    {
        roomsGrid = new BigSmallRoom[numberOfColumns, numberOfRows];
        gridPositions = new Stack<Vector2Int>();
    }

    private void Start()
    {
        CreateRooms();
        ConnectRooms();
    }

    private void CreateRooms()
    {
        var startingGridPos = new Vector2Int(0, numberOfRows - 1);
        var startingPosition = Vector2.zero;

        roomsGridParentObject = new GameObject("RoomsParent");

        var firstRoom = Instantiate(roomPrefab, startingPosition, Quaternion.identity);
        firstRoom.CloseDoors();
        firstRoom.transform.parent = roomsGridParentObject.transform;
        gridPositions.Push(startingGridPos);
        roomsGrid[startingGridPos.x, startingGridPos.y] = firstRoom;

        var chance = 0;
        var chanceCount = 0;
        while(gridPositions.Count > 0)
        {
            var currentGridPos = gridPositions.Pop();
            var currentPosition = roomsGrid[currentGridPos.x, currentGridPos.y].transform.position;
            var up = new Vector2Int(currentGridPos.x, currentGridPos.y - 1);
            var down = new Vector2Int(currentGridPos.x, currentGridPos.y + 1);
            var left = new Vector2Int(currentGridPos.x - 1, currentGridPos.y);
            var right = new Vector2Int(currentGridPos.x + 1, currentGridPos.y);

            var upPosition = currentPosition;
            upPosition.y += yPositionOffset;
            PlaceRoom(up, upPosition, Random.Range(0, 75) >= chance);

            var downPosition = currentPosition;
            downPosition.y -= yPositionOffset;
            PlaceRoom(down, downPosition, Random.Range(0, 69) >= chance);

            var leftPosition = currentPosition;
            leftPosition.x -= xPositionOffset;
            PlaceRoom(left, leftPosition, Random.Range(0, 65) >= chance);

            var rightPosition = currentPosition;
            rightPosition.x += xPositionOffset;
            PlaceRoom(right, rightPosition, Random.Range(0, 65) >= chance);

            chance += 5;

            if(chance >= 75)
            {
                chanceCount++;
                chance = chanceCount * 15;
            }
        }

        for(int colIndex = numberOfColumns - 1; colIndex >= 0; colIndex--)
        {
            for(int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
            {
                var room = roomsGrid[colIndex, rowIndex];
                if(room != null)
                {
                    var winRoom = Instantiate(winRoomPrefab, room.transform.position, Quaternion.identity);
                    winRoom.transform.parent = roomsGridParentObject.transform;
                    roomsGrid[colIndex, rowIndex] = winRoom;
                    winRoom.gameObject.SetActive(false);
                    Destroy(room.gameObject);
                    colIndex = 0;
                    break;
                }
            }

        }
    }

    private void PlaceRoom(Vector2Int roomGridPos, Vector2 pos, bool place)
    {
        if(!place)
        {
            return;
        }

        if(roomGridPos.x >= 0 && roomGridPos.x < numberOfColumns && roomGridPos.y >= 0 && roomGridPos.y < numberOfRows)
        {
            if(roomsGrid[roomGridPos.x, roomGridPos.y] == null)
            {
                var room = Instantiate(roomPrefab, pos, Quaternion.identity);
                roomsGrid[roomGridPos.x, roomGridPos.y] = room;
                room.transform.parent = roomsGridParentObject.transform;
                gridPositions.Push(roomGridPos);
                room.gameObject.SetActive(false);
            }
        }
    }

    private void ConnectRooms()
    {
        for(int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            for(int columnIndex = 0; columnIndex < numberOfColumns; columnIndex++)
            {
                var currentRoom = roomsGrid[columnIndex, rowIndex];
                if(currentRoom != null)
                {
                    var up = new Vector2Int(columnIndex, rowIndex - 1);
                    var down = new Vector2Int(columnIndex, rowIndex + 1);
                    var left = new Vector2Int(columnIndex - 1, rowIndex);
                    var right = new Vector2Int(columnIndex + 1, rowIndex);


                    ConnectIndividuals(up, currentRoom, 0);
                    ConnectIndividuals(down, currentRoom, 1);
                    ConnectIndividuals(left, currentRoom, 2);
                    ConnectIndividuals(right, currentRoom, 3);
                }
            }
        }
    }

    private void ConnectIndividuals(Vector2Int roomGridPos, BigSmallRoom currentRoom, int direction)
    {
        if(roomGridPos.x >= 0 && roomGridPos.x < numberOfColumns && roomGridPos.y >= 0 && roomGridPos.y < numberOfRows)
        {
            switch(direction)
            {
                case 0:
                    currentRoom.Up = roomsGrid[roomGridPos.x, roomGridPos.y];
                    break;
                case 1:
                    currentRoom.Down = roomsGrid[roomGridPos.x, roomGridPos.y];
                    break;
                case 2:
                    currentRoom.Left = roomsGrid[roomGridPos.x, roomGridPos.y];
                    break;
                case 3:
                    currentRoom.Right = roomsGrid[roomGridPos.x, roomGridPos.y];
                    break;
                default:
                    break;
            }
        }
    }
}
