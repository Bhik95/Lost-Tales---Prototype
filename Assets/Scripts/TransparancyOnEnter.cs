using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class TransparancyOnEnter : MonoBehaviour
{
    public float transp = .5f;

    float curT = 1;
    private void Update()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.Lerp(c, new Color(c.r, c.g, c.b, curT), Time.deltaTime*2);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == StaticVariables.Tags.Player)
        {
            curT = transp;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == StaticVariables.Tags.Player)
        {
            curT = 1;
        }
    }
}
