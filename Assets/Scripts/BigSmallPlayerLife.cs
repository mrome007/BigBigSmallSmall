using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BigSmallPlayerLife : MonoBehaviour 
{
    [SerializeField]
    private Text lifeText;

    private int life = 5;

    private SpriteRenderer spriteRenderer;
    private bool canBeHit = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        lifeText.text = life.ToString();
        ImmunityAtStart();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!canBeHit)
        {
            return;
        }
        if(other.gameObject.tag == "Enemy")
        {
            StartCoroutine(PlayerHurt());
            life--;
            lifeText.text = life.ToString();
            if(life < 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private IEnumerator PlayerHurt()
    {
        canBeHit = false;
        for(int index = 0; index < 10; index++)
        {
            var color = Color.red;
            color.a = 0.5f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
            color = Color.white;
            color.a = 0.5f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = Color.white;
        canBeHit = true;
    }

    private IEnumerator CantBeHitAtStart()
    {
        canBeHit = false;
        var color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
        canBeHit = true;
    }

    public void ImmunityAtStart()
    {
        StartCoroutine(CantBeHitAtStart());
    }
}
