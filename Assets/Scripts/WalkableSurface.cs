using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(PolygonCollider2D))]
public class WalkableSurface : MonoBehaviour
{
    public Transform Lantern;
    PolygonCollider2D col;
    void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        StartCoroutine(MoveColliders());
    }

    IEnumerator MoveColliders()
    {
        while (true)
        {
            var p = col.points.ToList();
            for (int i = 0; i < p.Count; i++)
            {

                var dir = p[i] - (Vector2)Lantern.position;
                if ((dir).sqrMagnitude < 1*1)
                {
                    float newDist = ((Vector2)transform.position - (p[i] + dir.normalized)).sqrMagnitude;
                    float oldDist = ((Vector2)transform.position - p[i]).sqrMagnitude;
                    if (oldDist < newDist)
                    {
                        p[i] += dir.normalized;
                    }
                    else
                    {
                        p[i] += (p[i] - (Vector2)transform.position).normalized;
                    }


                    if (i < p.Count - 1)
                    {
                        dir = p[i] - p[i + 1];
                        if ((dir).sqrMagnitude > .1f)
                        {
                            p.Add(p[i] + (dir * .5f));
                        }
                    }
                    
                }
            }
            col.points = p.ToArray();
            yield return null;
        }
    }
}
