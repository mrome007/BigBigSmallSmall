using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallSpriteChange : MonoBehaviour 
{
    [SerializeField]
    private Sprite upSprite;

    [SerializeField]
    private Sprite downSprite;

    [SerializeField]
    private Sprite leftSprite;

    [SerializeField]
    private Sprite rightSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(bool horizontal, bool positive)
    {
        if(horizontal)
        {
            spriteRenderer.sprite = positive ? rightSprite : leftSprite;
        }
        else
        {
            spriteRenderer.sprite = positive ? upSprite : downSprite;
        }
    }
}
