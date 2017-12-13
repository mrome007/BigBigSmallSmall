using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBigOrSmall : MonoBehaviour 
{	
    private BigSmallMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<BigSmallMovement>();
        if(playerMovement == null)
        {
            Debug.LogError("No BigSmallMovement component");
        }
    }

    private void Update()
    {
        BigSmallInput();
    }

    private void BigSmallInput()
    {
        if(Input.GetKey(KeyCode.J))
        {
            GetBigSmall(false);
        }

        if(Input.GetKey(KeyCode.K))
        {
            GetBigSmall(true);
        }
    }

    private void GetBigSmall(bool small)
    {
        var scale = transform.localScale;
        if(small)
        {
            if(scale.x > 1)
            {
                scale.x--;
                scale.y--;
                transform.localScale = scale;
                playerMovement.Speed += 0.5f;
            }
        }
        else
        {
            if(scale.x < 25)
            {
                scale.x++;
                scale.y++;
                transform.localScale = scale;
                playerMovement.Speed -= 0.5f;
            }
        }
    }
}
