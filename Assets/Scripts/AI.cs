using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Interactible
{
    int index = 0;
    [SerializeField] private Animator Animator;
    [SerializeField] private float MovementSpeed = 100;
    [SerializeField] private float Radius = 0.2f;
    [SerializeField] private float CoordinateScaleFactor = 0.7f;
    [SerializeField] private FMODUnity.StudioEventEmitter _success_sound;
    [SerializeField] private FMODUnity.StudioEventEmitter _fail_sound;
    private Vector2 MoveDir;
    public Transform CurTarget;
    public GameObject PaintSprite;
    public GameObject ParticleInteract;
    private Transform[] trail = null; //populate it automatically by script

    [SerializeField] private FMODUnity.StudioEventEmitter FootstepsEmitter;
    [SerializeField] private float FootstepDelay = 1f;
    private Vector2 _last_direction;//It´s used to memorize the direction the sprite is facing (for the idle animations)
    private float _footstep_delay_timer = 0f;

    protected override void Interact()
    {
        //Debug.Log("AI: " + transform.parent.name);
        if (PlayerStatus.Instance.HadBigFlame && !ParticleInteract.activeInHierarchy)
        {
            _success_sound.Play();
            ParticleInteract.SetActive(true);
            base.Interact();
        }
        else
        {
            _fail_sound.Play();
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (CurTarget != null)
        {
            if (PaintSprite)
            {
                PaintSprite.transform.localScale = 
                    Vector3.MoveTowards(PaintSprite.transform.localScale, //current
                    new Vector3(1.2f,1.2f,1), //target
                    Time.deltaTime); //speed
            }
            MoveDir = CurTarget.position - transform.position;
            if (MoveDir.sqrMagnitude < Radius * Radius)
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
            if (_footstep_delay_timer <= 0)
            {
                _footstep_delay_timer = FootstepDelay;
                FootstepsEmitter.Play();
            }
            
            MoveDir.Normalize();
            _last_direction = MoveDir;
            SetAnimatorData();
            GetComponent<Rigidbody2D>().velocity = new Vector2(MoveDir.x, MoveDir.y) * Time.deltaTime * MovementSpeed;

            _footstep_delay_timer -= Time.fixedDeltaTime;
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
        //Animator?.SetFloat("Magnitude", CurTarget != null ? MoveDir.magnitude : 0);
        Animator?.SetFloat("Magnitude", CurTarget != null ? 1 : 0);
        Animator?.SetFloat("LastHorizontal", _last_direction.x);
        Animator?.SetFloat("LastVertical", _last_direction.y);
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.5f);
        CurTarget = trail[0];
    }
    protected override void OnActivate()
    {
        if(trail == null)
            PopulateTrail();
        StartCoroutine(LateStart());
    }

    private void PopulateTrail()
    {
        Transform trail_transform = transform.parent.GetChild(0);//AI.cs is in child of index 1...
        trail = new Transform[trail_transform.childCount];//... find child of index 0 and check for the grand-children
        for (int i = 0; i < trail_transform.childCount; i++)
        {
            trail[i] = trail_transform.GetChild(i);
        }
    }

    protected override void OnPlayerFar()
    {

    }

    protected override void OnPlayerNearby()
    {

    }
}
