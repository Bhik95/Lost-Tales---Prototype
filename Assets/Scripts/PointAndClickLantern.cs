using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickLantern : MonoBehaviour
{
    private Transform FollowTarget;

    [SerializeField] private Transform Target;
    [SerializeField] private Transform Player;
    [SerializeField] private float followSpeed;

    [SerializeField] private ParticleSystem ExplodeParticleSystem;
    [SerializeField] private ParticleSystem AimParticleSystem;
    [SerializeField] private ParticleSystem ShootParticleSystem;

    private Vector2 dirToMouse { get { return Camera.main.ScreenPointToRay(Input.mousePosition).origin - Player.position; } }


    void Start()
    {
        FollowTarget = Player;
        if (AimParticleSystem != null)
        {
            ParticleSystem.EmissionModule emission = AimParticleSystem.emission;
            emission.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FollowTarget != Player)
            {
                ChangeTarget(Player);
            }
            else
            {
                Target.position = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
                ChangeTarget(Target);
            }
        }

        Move();
    }

    void Move()
    {
        Vector2 offset = FollowTarget != Player ? Vector2.zero : Vector2.ClampMagnitude(-dirToMouse * 2, 1f) * .5f; //The lantern leans towards the mouse position for a better feel
        transform.GetComponent<Rigidbody2D>().position = Vector3.Lerp(transform.position, FollowTarget.position - (Vector3)offset, Time.deltaTime * followSpeed);

        if (AimParticleSystem != null)
        {
            ParticleSystem.EmissionModule emission = AimParticleSystem.emission;
            emission.enabled = (transform.position - FollowTarget.position).sqrMagnitude > .5f;
        }
    }

    public void ChangeTarget(Transform pTransform)
    {
        FollowTarget = pTransform;
        ShootParticleSystem.transform.LookAt(pTransform.position);
        ShootParticleSystem.Play();
    }
}