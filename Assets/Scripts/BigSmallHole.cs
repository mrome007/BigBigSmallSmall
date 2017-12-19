using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallHole : MonoBehaviour 
{
    [SerializeField]
    private float maxScaleToHold = 0.5f;

    public bool Done { get; private set; }

    public event EventHandler Filled;

    private SpriteRenderer spriteRenderer;
    private Collider2D buttonCollider;
    private float holdHold = 0.7f;
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
            if(other.transform.localScale.x <= maxScaleToHold)
            {
                var scale = other.transform.localScale.x;
                Destroy(other.gameObject);
                holdHold -= scale;
                HoleDone();
                Debug.Log("wat");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!Done)
        {
            if(other.tag == "Enemy")
            {
                if(other.transform.localScale.x <= maxScaleToHold)
                {
                    var scale = other.transform.localScale.x;
                    Destroy(other.gameObject);
                    holdHold -= scale;
                    HoleDone();
                    Debug.Log("hello");
                }
            }
        }
    }

    private void HoleDone()
    {
        Debug.Log(holdHold);
        if(holdHold <= 0f)
        {
            Done = true;
            var handler = Filled;
            if(handler != null)
            {
                handler(this, null);
            }
            buttonCollider.enabled = false;
            ChangeHole();
        }
    }

    private void OnEnable()
    {
        ChangeHole();
    }

    private void ChangeHole()
    {
        spriteRenderer.sprite = Done ? BigSmallRoomObjects.Instance.Filled : BigSmallRoomObjects.Instance.Hole;
    }
}
