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
        bigSmallRoom = transform.parent.GetComponent<BigSmallRoom>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var pos = Vector3.zero;
        var playerPos = transform.position;
        switch(doorDirection)
        {
            case DoorDirection.Up:
                if(bigSmallRoom.Up == null)
                {
                    return;
                }
                pos = bigSmallRoom.Up.transform.position;
                playerPos.y += 2.5f;
                break;

            case DoorDirection.Down:
                if(bigSmallRoom.Down == null)
                {
                    return;
                }
                pos = bigSmallRoom.Down.transform.position;
                playerPos.y -= 2.5f;
                break;

            case DoorDirection.Left:
                if(bigSmallRoom.Left == null)
                {
                    return;
                }
                pos = bigSmallRoom.Left.transform.position;
                playerPos.x -= 2.5f;
                break;

            case DoorDirection.Right:
                if(bigSmallRoom.Right == null)
                {
                    return;
                }
                pos = bigSmallRoom.Right.transform.position;
                playerPos.x += 2.5f;
                break;
        }

        var cam = Camera.main.GetComponent<CameraUtility>();
        cam.MoveCamera(pos);

        bigSmallRoom.PlayerTransform.position = playerPos;
    }
}
