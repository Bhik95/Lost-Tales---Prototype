#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;
    [SerializeField]private float MovementSpeed = 100;
    [SerializeField] private float CoordinateScaleFactor = 0.7f;
    [SerializeField] private FMODUnity.StudioEventEmitter FootstepsEmitter;
    [SerializeField] private float FootstepDelay = 1f;
    private float _footstep_delay_timer = 0f;
    public Vector2 GetInput;

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        SetAnimatorData();
        Vector2 move = Vector2.ClampMagnitude(GetInput, 1);

        if(move.magnitude > 0.01)
        {
            if (_footstep_delay_timer <= 0)
            {
                _footstep_delay_timer = FootstepDelay;
                FootstepsEmitter.Play();
            }
        }
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(move.x, move.y * CoordinateScaleFactor) * Time.fixedDeltaTime * MovementSpeed;
        //transform.Translate(GetInput.normalized * Time.deltaTime * 2);

        _footstep_delay_timer -= Time.fixedDeltaTime;
    }

    void SetAnimatorData()
    {
        Animator?.SetFloat("Horizontal", GetInput.x);
        Animator?.SetFloat("Vertical", GetInput.y);
        Animator?.SetFloat("Magnitude", GetInput.magnitude);
    }
}
