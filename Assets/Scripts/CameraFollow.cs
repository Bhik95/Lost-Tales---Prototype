using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x,Target.position.y,transform.position.z), Time.deltaTime * 2);
        }
    }
}
