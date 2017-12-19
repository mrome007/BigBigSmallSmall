using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallRoomObjects : MonoBehaviour 
{
    public static BigSmallRoomObjects Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (BigSmallRoomObjects)FindObjectOfType(typeof(BigSmallRoomObjects));
            }
            return instance;
        }
    }

    private static BigSmallRoomObjects instance = null;
    public List<BigSmallEnemy> BigSmallEnemyObjects;
    public BigSmallHole BigSmallHoleObject;
    public BigSmallButton BigSmallButtonObject;
    public Sprite UpButton;
    public Sprite DownButton;

    private void Awake()
    {
        if(instance == null)
        {
            instance = (BigSmallRoomObjects)FindObjectOfType(typeof(BigSmallRoomObjects));
        }
    }
}
