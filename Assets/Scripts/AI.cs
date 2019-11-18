using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Interactible
{
    int index = 0;
    [SerializeField] private Transform[] trail;
    [SerializeField] private Animator Animator;
    [SerializeField] private float MovementSpeed = 100;
    [SerializeField] private float CoordinateScaleFactor = 0.7f;
    private Vector2 MoveDir;
    public Transform CurTarget;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (CurTarget != null)
        {
            MoveDir = CurTarget.position - transform.position;
            if (MoveDir.sqrMagnitude < .2f)
            {
                index++;
                if (index < trail.Length)
                {
                    CurTarget = trail[index];
                }
                else
                {
                    CurTarget = null;
                }
            }
            SetAnimatorData();
            MoveDir.Normalize();
            GetComponent<Rigidbody2D>().velocity = new Vector2(MoveDir.x, MoveDir.y * CoordinateScaleFactor) * Time.deltaTime * MovementSpeed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }

    void SetAnimatorData()
    {
        Animator?.SetFloat("Horizontal", MoveDir.x);
        Animator?.SetFloat("Vertical", MoveDir.y);
        Animator?.SetFloat("Magnitude", MoveDir.magnitude);
    }
    
    protected override void OnActivate()
    {
        CurTarget = trail[0];
    }

    protected override void OnPlayerFar()
    {

    }

    protected override void OnPlayerNearby()
    {

    }
}
