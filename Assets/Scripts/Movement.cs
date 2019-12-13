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
    private Vector2 _last_direction;//It´s used to memorize the direction the sprite is facing (for the idle animations)
    private float _footstep_delay_timer = 0f;
    public Vector2 GetInput;

    public bool ControlsEnabled = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        SetAnimatorData();
        Vector2 move = ControlsEnabled ? Vector2.ClampMagnitude(GetInput, 1) : Vector2.zero;

        if(move.magnitude > 0.01)
        {
            _last_direction = move;

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
        Animator?.SetFloat("Horizontal", ControlsEnabled ? GetInput.x : 0);
        Animator?.SetFloat("Vertical", ControlsEnabled ? GetInput.y : 0);
        Animator?.SetFloat("Magnitude", ControlsEnabled ? GetInput.magnitude : 0);
        Animator?.SetFloat("LastHorizontal", _last_direction.x);
        Animator?.SetFloat("LastVertical", _last_direction.y);
    }
}
