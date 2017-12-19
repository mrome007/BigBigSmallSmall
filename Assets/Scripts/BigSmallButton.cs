using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallButton : MonoBehaviour 
{
    [SerializeField]
    private float maxScaleToHold = 1.75f;
    
    public bool Done { get; private set; }
    
    public event EventHandler Pressed;

    private SpriteRenderer spriteRenderer;
    private Collider2D buttonCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buttonCollider = GetComponent<Collider2D>();
        Done = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if(other.transform.localScale.x > maxScaleToHold)
            {
                ButtonDone();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!Done)
        {
            if(other.tag == "Enemy")
            {
                if(other.transform.localScale.x > maxScaleToHold)
                {
                    ButtonDone();
                }
            }
        }
    }

    private void ButtonDone()
    {
        Done = true;
        var handler = Pressed;
        if(handler != null)
        {
            handler(this, null);
        }
        buttonCollider.enabled = false;
        ChangeButton();
    }

    private void OnEnable()
    {
        ChangeButton();
    }

    private void ChangeButton()
    {
        spriteRenderer.sprite = Done ? BigSmallRoomObjects.Instance.DownButton : BigSmallRoomObjects.Instance.UpButton;
    }
}
