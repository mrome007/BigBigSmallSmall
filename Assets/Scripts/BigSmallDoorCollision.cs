using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallDoorCollision : MonoBehaviour 
{
    public enum DoorDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    private DoorDirection doorDirection;

    private BigSmallRoom bigSmallRoom;

    private void Awake()
    {
        bigSmallRoom = transform.parent.parent.GetComponent<BigSmallRoom>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
        {
            return;
        }

        var pos = Vector3.zero;
        var playerPos = transform.position;
        BigSmallRoom dir = null;
        switch(doorDirection)
        {
            case DoorDirection.Up:
                if(bigSmallRoom.Up == null)
                {
                    return;
                }
                dir = bigSmallRoom.Up;
                pos = bigSmallRoom.Up.transform.position;
                playerPos.y += 3f;
                break;

            case DoorDirection.Down:
                if(bigSmallRoom.Down == null)
                {
                    return;
                }
                dir = bigSmallRoom.Down;
                pos = bigSmallRoom.Down.transform.position;
                playerPos.y -= 3f;
                break;

            case DoorDirection.Left:
                if(bigSmallRoom.Left == null)
                {
                    return;
                }
                dir = bigSmallRoom.Left;
                pos = bigSmallRoom.Left.transform.position;
                playerPos.x -= 3f;
                break;

            case DoorDirection.Right:
                if(bigSmallRoom.Right == null)
                {
                    return;
                }
                dir = bigSmallRoom.Right;
                pos = bigSmallRoom.Right.transform.position;
                playerPos.x += 3f;
                break;
        }

        var cam = Camera.main.GetComponent<CameraUtility>();
        cam.MoveCamera(pos, bigSmallRoom, dir);

        BigSmallRoom.PlayerTransform.position = playerPos;
    }
}
