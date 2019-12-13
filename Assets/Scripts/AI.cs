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
    [SerializeField] private FMODUnity.StudioEventEmitter _success_sound;
    [SerializeField] private FMODUnity.StudioEventEmitter _fail_sound;
    private Vector2 MoveDir;
    public Transform CurTarget;
    public GameObject PaintSprite;
    public GameObject ParticleInteract;

    protected override void Interact()
    {
        if (PlayerStatus.Instance.HasBigFlame && !ParticleInteract.activeInHierarchy)
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
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.5f);
        CurTarget = trail[0];
    }
    protected override void OnActivate()
    {
        StartCoroutine(LateStart());
    }

    protected override void OnPlayerFar()
    {

    }

    protected override void OnPlayerNearby()
    {

    }
}
