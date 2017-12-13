using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBigSmallProjectile : MonoBehaviour 
{
    [SerializeField]
    private BigSmallProjectile smallProjectile;

    [SerializeField]
    private BigSmallProjectile bigProjectile;

    private void Update()
    {
        FireBigSmallInput();
    }

    private void FireBigSmallInput()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            var big = Instantiate(bigProjectile, transform.position, Quaternion.identity);
            big.Initialize(transform.localScale.x, false);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            var small = Instantiate(smallProjectile, transform.position, Quaternion.identity);
            small.Initialize(transform.localScale.x, true);
        }
    }
}
