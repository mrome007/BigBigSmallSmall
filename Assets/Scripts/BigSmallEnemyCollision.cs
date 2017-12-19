using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallEnemyCollision : MonoBehaviour 
{
    private float maxScale = 3f;
    private float minScale = 0.1f;

    private float bigIncr = 0.2f;
    private float smallIncr = 0.1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Big")
        {
            var scale = transform.localScale;
            if(scale.x < maxScale)
            {
                scale.x += bigIncr;
                scale.y += bigIncr;
                transform.localScale = scale;
            }
            Destroy(other.gameObject);
        }
        else if(other.tag == "Small")
        {
            var scale = transform.localScale;
            if(scale.x > minScale)
            {
                scale.x -= smallIncr;
                scale.y -= smallIncr;
                transform.localScale = scale;
            }
            Destroy(other.gameObject);
        }

    }
}
