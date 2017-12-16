using System.Collections;
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
    }
}
