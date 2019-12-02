using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Transform TempTarget;
    private Vector3 FollowPos;

    [SerializeField]bool drawGizmos;
    [SerializeField]Vector2 center;
    [SerializeField]float maxX, maxY;

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
            if (Mathf.Abs(FollowPos.x) >= maxX + Mathf.Abs( center.x))
            {
                FollowPos.x = FollowPos.x < 0 ? -maxX + center.x: maxX + center.x;
            }
            if (Mathf.Abs(FollowPos.y) >= maxY + Mathf.Abs( center.y))
            {
                FollowPos.y = FollowPos.y < 0 ? -maxY + center.y : maxY + center.y;
            }
        }

        FollowPos.z = -10f;
        transform.position = Vector3.Lerp(transform.position, FollowPos, Time.deltaTime * 2);

    }

    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, new Vector3(maxX*2, maxY*2, 1));
            Gizmos.DrawWireSphere(center, 1f);
        }
        
    }
}
