using System.Collections;
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
    private float damage;
    private BigSmall projectileType;

    public void Initialize(float scale, bool small)
    {
        projectileType = small ? BigSmall.Small : BigSmall.Big;
        damage = scale;
        var maxSpeed = (float)((int)scale / 2) + 3f;
        speed = maxSpeed - (float)((int)scale / 2);
        var projScale = scale / 4f + 0.25f;
        transform.localScale = new Vector3(projScale, projScale, 1f);
    }
}
