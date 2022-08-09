using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadinColor : MonoBehaviour
{
    private SpriteRenderer spriteManager;

    private void Start()
    {
        spriteManager = GetComponent<SpriteRenderer>();
    }
    public void ChangeFading()
    {
        Color newcolor = spriteManager.color;
        if (newcolor.a > 0.2f)
            newcolor.a -= 0.2f;
        else
            newcolor.a = 0.2f;
        spriteManager.color = newcolor;
    }
    public void MoreFading()
    {
        Color newcolor = spriteManager.color;
        if (newcolor.a < 1)
        {
            newcolor.a += 0.2f;
            spriteManager.color = newcolor;
        }
    }
    public void SetFading(float number)
    {
        Color newcolor = spriteManager.color;
        newcolor.a = number;
        spriteManager.color = newcolor;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            SetFading(0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            SetFading(1f);
        }
    }
}
