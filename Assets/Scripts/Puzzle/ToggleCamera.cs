using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class ToggleCamera : MonoBehaviour
{
    [Header("No transform removes temp target from camera")]
    [SerializeField] Transform Transform;
    void OnTriggerEnter2D(Collider2D col)
    {
      
            Camera.main.GetComponentInParent<CameraFollow>().TempTarget = Transform;
    }
}
