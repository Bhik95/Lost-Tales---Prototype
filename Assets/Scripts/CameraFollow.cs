﻿using System.Collections;
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

    public bool IgnoreDistanceCheck { get; private set; }

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
            if (!IgnoreDistanceCheck && dirToTarget.magnitude >= 10)
            {
                TempTarget = null;
            }
            FollowPos += dirToTarget.normalized * Mathf.Min(dirToTarget.magnitude / 3.0f, 1);
        }
        else if (Target)
        {
            FollowPos = new Vector3(Target.position.x, Target.position.y, transform.position.z);
            var t = FollowPos;
            if (t.x >= center.x + maxX)
            {
                t.x = center.x + maxX;
            }

            if (t.x < center.x - maxX)
            {
                t.x = center.x - maxX;
            }
            if (t.y >= center.y + maxY)
            {
                t.y = center.y + maxY;
            }

            if (t.y < center.y - maxY)
            {
                t.y = center.y - maxY;
            }
            FollowPos = t;
        }

        FollowPos.z = -10f;
        transform.position = Vector3.Lerp(transform.position, FollowPos, Time.deltaTime * 2);

    }

    public void ResetAfterTimeout(float duration, bool disablePlayerControls)
    {
        StartCoroutine(ResetAfterTimeoutCoroutine(duration, disablePlayerControls));
    }

    private IEnumerator ResetAfterTimeoutCoroutine(float duration, bool disablePlayerControls)
    {
        IgnoreDistanceCheck = true;
        if(disablePlayerControls)
            PlayerStatus.Instance.GetComponent<Movement>().ControlsEnabled = false;
        float timer = duration;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        TempTarget = null;
        IgnoreDistanceCheck = false;
        if(disablePlayerControls)
            PlayerStatus.Instance.GetComponent<Movement>().ControlsEnabled = true;
    }

    public void SetTempTargetAndResetAfterTimeout(Transform tempTarget, float duration, bool disablePlayerControls)
    {
        this.TempTarget = tempTarget;
        ResetAfterTimeout(duration, disablePlayerControls);
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
