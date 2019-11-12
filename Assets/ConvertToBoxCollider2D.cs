using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToBoxCollider2D : MonoBehaviour
{
    [ContextMenu("Convert")]
    public void Convert()
    {
        BoxCollider b3d = GetComponent<BoxCollider>();
        BoxCollider2D b2d = gameObject.AddComponent<BoxCollider2D>();

        b2d.offset = b3d.center;
        b2d.size = b3d.size;

        Destroy(b3d);
    }
}
