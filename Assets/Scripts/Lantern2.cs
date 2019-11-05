using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern2 : MonoBehaviour
{
    Quaternion startRot;
    public enum LanternState
    {
        following, charging, moving, placed
    }
    public LanternState CurrentState = LanternState.following;

    private Vector2 PlayerMoveDirection;
    private Vector3 velocity;

    [SerializeField] private Transform Player;
    [SerializeField] private float followSpeed;
    [SerializeField] private float shootSpeed;

    [SerializeField] private Texture2D CursorStandard;
    [SerializeField] private Texture2D CursorAim;
    [SerializeField] private Texture2D CursorAim2;

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
                //offset = PlayerMoveDirection.magnitude > 0 ? Vector2.zero : (Vector2.down + Vector2.right).normalized * .3f; //Previous Pekko´s code (fixed offset)
                offset = PlayerMoveDirection.magnitude > 0 ? Vector2.zero : Vector2.ClampMagnitude(-dirToMouse*2, 1f) * .5f; //The lantern leans towards the mouse position for a better feel
                transform.GetComponent<Rigidbody2D>().position = Vector3.Lerp(transform.position, Player.position - (Vector3)offset, Time.deltaTime * 3.5f);


                if (!ReadyToShoot && Vector3.Distance(transform.position, Player.position) < 1)
                {
                    ReadyToShoot = true;
                }
                if (ReadyToShoot && Input.GetMouseButtonDown(0))
                {
                    ReadyToShoot = false;
                    ChangeState(LanternState.charging);
                }
                break;
            case LanternState.charging://update charging

                offset = dirToMouse.normalized * .3f;
                transform.position = Vector3.Lerp(transform.position, Player.position + (Vector3)offset, Time.deltaTime * 5);
                AimParticleSystem.transform.LookAt(Player.position);
                if (Input.GetMouseButtonUp(0))
                {
                    if (AimParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule emission = AimParticleSystem.emission;
                        emission.enabled = false;
                    }
                    ChangeState(LanternState.moving);
                    Cursor.SetCursor(CursorStandard, Vector2.zero, CursorMode.ForceSoftware);

                }
                break;
            case LanternState.moving://update moving/shooting
                GetComponent<Rigidbody2D>().velocity = velocity;
                
                //if (velocity.magnitude < 0.01f || Vector3.Distance(transform.position, Player.position) > 3f || (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, Player.position) > .5f))
                if (velocity.magnitude < 0.01f)
                {
                    //StartCoroutine(StartRecall());
                    ChangeState(LanternState.placed);
                }
                break;
            case LanternState.placed://update idle
                if (Input.GetMouseButtonUp(1))
                {
                    ExplodeParticleSystem.Play();
                    ChangeState(LanternState.following);
                }
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
            case LanternState.charging: //start charging lantern
                CursorToggle = StartCoroutine(ToggleCursor());
                if (AimParticleSystem != null)
                {
                    ParticleSystem.EmissionModule emission = AimParticleSystem.emission;
                    emission.enabled = true;
                }
                break;
            case LanternState.moving: //start moving/shooting
                StopCoroutine(CursorToggle);
                ShootParticleSystem.transform.LookAt(Player.position);
                ShootParticleSystem.Play();
                velocity = dirToMouse.normalized * shootSpeed;
                break;
            case LanternState.placed: //start idle
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().velocity *= 0;
                break;
        }
        CurrentState = pLanternState;
    }

    void ProcessInputs()
    {
        PlayerMoveDirection = Player.GetComponent<Movement>().GetInput;
        //AimDirection = PlayerMoveDirection.magnitude > 0 ? PlayerMoveDirection : AimDirection; //last valid input
    }

    /*IEnumerator StartRecall()
    {
        ExplodeParticleSystem.Play();
        yield return new WaitForSeconds(.4f);
        ChangeState(LanternState.following);
    }*/

    IEnumerator ToggleCursor()
    {
        while (true)
        {
            Cursor.SetCursor(CursorAim, Vector2.zero, CursorMode.ForceSoftware);
            yield return new WaitForSeconds(.5f);
            Cursor.SetCursor(CursorAim2, Vector2.zero, CursorMode.ForceSoftware);
            yield return new WaitForSeconds(.5f);
        }
    }
}
