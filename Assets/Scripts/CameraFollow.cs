using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Transform TempTarget;
    private Vector3 FollowPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //if (Target != null)
        //{
        //    FollowPos = new Vector3(Target.position.x, Target.position.y, transform.position.z);   
        //}

        if (TempTarget && Target)
        {
            FollowPos = TempTarget.position;
            var dirToTarget = (Target.position - TempTarget.position);
            if (dirToTarget.magnitude >= 5)
            {
                TempTarget = null;
            }
            FollowPos += dirToTarget.normalized * Mathf.Min(dirToTarget.magnitude / 3.0f, 1);
        }
        else if (Target)
        {
            FollowPos = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, FollowPos, Time.deltaTime * 2);

    }
}
