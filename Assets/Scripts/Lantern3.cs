#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern3 : MonoBehaviour
{
    Quaternion startRot;
    public enum LanternState
    {
        following, idle
    }
    public LanternState CurrentState = LanternState.following;

    private Vector2 PlayerMoveDirection;
    private Vector3 velocity;

    [SerializeField] private Transform Player;
    [SerializeField] private float followSpeed;

    [SerializeField] private ParticleSystem ExplodeParticleSystem;
    [SerializeField] private ParticleSystem AimParticleSystem;
    [SerializeField] private ParticleSystem ShootParticleSystem;

    private bool ReadyToShoot = false;
    private Coroutine CursorToggle;
    private Vector2 dirToMouse{get{ return Camera.main.ScreenPointToRay(Input.mousePosition).origin - Player.position; }}

    private void Start()
    {
        startRot = transform.rotation;
        if (AimParticleSystem != null)
        {
            ParticleSystem.EmissionModule emission = AimParticleSystem.emission;
            emission.enabled = false;
        }
    }

    void Update()
    {
        var offset = Vector2.zero;
        switch (CurrentState)
        {

            case LanternState.following: //update following
                transform.rotation = Quaternion.Slerp(transform.rotation, startRot, Time.deltaTime * 10);
                offset = PlayerMoveDirection.magnitude > 0 ? Vector2.zero : Vector2.ClampMagnitude(-dirToMouse * 2, 1f) * .5f; //The lantern leans towards the mouse position for a better feel
                transform.GetComponent<Rigidbody2D>().position = Vector3.Lerp(transform.position, Player.position - (Vector3)offset, Time.deltaTime * 5);

                if (!ReadyToShoot && Vector3.Distance(transform.position, Player.position) < 1)
                {
                    ReadyToShoot = true;
                }
                if (ReadyToShoot && Input.GetMouseButtonDown(0))
                {
                    ReadyToShoot = false;
                }
                break;
            case LanternState.idle://update idle
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        velocity *= .9f;
    }

    void ChangeState(LanternState pLanternState)
    {
        switch (pLanternState)
        {
            case LanternState.following: //start following
                velocity = Vector3.zero;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().isKinematic = false;

                break;
            case LanternState.idle: //start idle
                break;
        }
        CurrentState = pLanternState;
    }

    void ProcessInputs()
    {
        PlayerMoveDirection = Player.GetComponent<Movement>().GetInput;
        //AimDirection = PlayerMoveDirection.magnitude > 0 ? PlayerMoveDirection : AimDirection; //last valid input
    }

}
