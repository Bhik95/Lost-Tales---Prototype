using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for(int i=0; i<transform.childCount - 1;i++)
        {
            float c = i / (float)(transform.childCount - 1);
            Gizmos.color = new Color(0, c, 1 - c, 0.5f);
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.1f);
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        Gizmos.color = new Color(1, 1, 0, 0.5f); //LAst one is yellow
        Gizmos.DrawSphere(transform.GetChild(transform.childCount - 1).position, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        //Draw white circles around the checkpoints
        Gizmos.color = Color.white;
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.15f);
        }
    }

    private void Start()
    {
        Destroy(this);//Remove for performance reasons
    }
}
