using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallRoomColliderController : MonoBehaviour 
{
    [SerializeField]
    private DoorColliders up;

    [SerializeField]
    public DoorColliders down;

    [SerializeField]
    public DoorColliders left;

    [SerializeField]
    public DoorColliders right;
   
    public void EnableDoors(bool hasUp, bool hasDown, bool hasLeft, bool hasRight)
    {
        EnableIndividualDoors(up, hasUp);
        EnableIndividualDoors(down, hasDown);
        EnableIndividualDoors(left, hasLeft);
        EnableIndividualDoors(right, hasRight);
    }

    private void EnableIndividualDoors(DoorColliders door, bool hasDoor)
    {
        door.DoorCollider.gameObject.SetActive(hasDoor);
        door.BlockerCollider.gameObject.SetActive(!hasDoor);
    }
}

[Serializable]
public class DoorColliders
{
    public Collider2D DoorCollider;
    public Collider2D BlockerCollider;
}
