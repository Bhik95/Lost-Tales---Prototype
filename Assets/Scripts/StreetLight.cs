using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _light_go;

    [SerializeField] private FMODUnity.StudioEventEmitter _fmod_event_emitter;
    [SerializeField] private float _sound_distance_min;
    [SerializeField] private float _sound_distance_max;
    [SerializeField] private float _sound_transition_duration = 1f;

    public float Cooldown = 3.0f;
    private float _timer = 0f;
    private bool _lit = false;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;

        if (!_lit)
        {
            float goalProximity = Mathf.Clamp01((Vector2.Distance(_player.transform.position, transform.position) - _sound_distance_min) / (_sound_distance_max - _sound_distance_min));
            _fmod_event_emitter.SetParameter("GoalProximity", 1 - goalProximity);
        }
        
        //Debug.Log(goalProximity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_timer <= 0 && collision.gameObject.layer == LayerMask.NameToLayer("Lantern"))
        {
            _lit = !_lit;

            _timer = Cooldown;

            StartCoroutine(ChangeMoodValue("Mood", _lit ? 0 : 1, _lit ? 1 : 0, _sound_transition_duration));
            StartCoroutine(ChangeMoodValue("GoalProximity", 1 - Mathf.Clamp01((Vector2.Distance(_player.transform.position, transform.position) - _sound_distance_min) / (_sound_distance_max - _sound_distance_min)), 0, _sound_transition_duration));
            _light_go.SetActive(_lit);
            _animator.SetBool("Lit", _lit);
        }
    }

    private IEnumerator ChangeMoodValue(string paramName, float startValue, float finalValue, float duration)
    {
        float timer = 0;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            _fmod_event_emitter.SetParameter(paramName, Mathf.Lerp(startValue, finalValue, timer / duration));
            yield return null;
        }
        _fmod_event_emitter.SetParameter(paramName, finalValue);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sound_distance_min);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sound_distance_max);
    }
}
