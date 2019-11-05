using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    private enum PatrolState
    {
        PATROL, WAIT, CHASE
    }

    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _speed = 0.4f;
    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private float _max_timer = 3f;


    private PatrolState _state = PatrolState.PATROL;
    private int _patrol_i = 0;

    private float _timer;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case PatrolState.PATROL:
                if(_patrolPoints.Length > 0)
                {
                    if((transform.position - _patrolPoints[_patrol_i].position).sqrMagnitude <= _radius * _radius)
                    {
                        _patrol_i = (_patrol_i + 1) % _patrolPoints.Length;
                        _state = PatrolState.WAIT;
                        _timer = _max_timer;
                        _rb.velocity *= 0;
                    }
                    else
                    {
                        _rb.velocity = ((_patrolPoints[_patrol_i].position - transform.position)).normalized * _speed;
                    }
                }
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.7f); //Anisotropic coordinate system factor;
                break;
            case PatrolState.WAIT:
                if(_timer <= 0)
                {
                    _state = PatrolState.PATROL;
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
                break;
            case PatrolState.CHASE:
                break;
        }
    }
}
