#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarMusicChange : Interactible
{
    [SerializeField] private FMODUnity.StudioEventEmitter _music_emitter;
    [SerializeField] private FMODUnity.StudioEventEmitter _interact_sound_emitter;
    [SerializeField] private float _sound_distance_min;
    [SerializeField] private float _sound_distance_max;
    [SerializeField] private float _sound_transition_duration = 1f;

    [SerializeField] private GameObject _Light;
    [SerializeField] private GameObject _maze;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private GameObject _particleSystemEffector;


    private bool _lit = false;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        base.Update();
        float goalProximity = Mathf.Clamp01((Vector2.Distance(_player.transform.position, transform.position) - _sound_distance_min) / (_sound_distance_max - _sound_distance_min));
        _music_emitter.SetParameter("GoalProximity", 1 - goalProximity);
        //Debug.Log(goalProximity);
    }

    private IEnumerator ChangeMoodValue(string paramName, float startValue, float finalValue, float duration)
    {
        float timer = 0;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            _music_emitter.SetParameter(paramName, Mathf.Lerp(startValue, finalValue, timer / duration));
            yield return null;
        }
        _music_emitter.SetParameter(paramName, finalValue);
    }

    private IEnumerator TempFollowPlayer()
    {
        float timer = 0;
        while (timer < 3)
        {
            if (_Light)
            {
                _Light.transform.localScale = Vector3.MoveTowards(_Light.transform.localScale, new Vector3(1.2f,1.2f,1),Time.deltaTime);
            }

            timer += Time.deltaTime;
            _particleSystemEffector.transform.position = _player.transform.position;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sound_distance_min);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sound_distance_max);
    }

    protected override void OnPlayerNearby()
    {
        
    }

    protected override void OnPlayerFar()
    {
        
    }

    protected override void OnActivate()
    {
        if (!_lit)
        {
            _lit = !_lit;

            StartCoroutine(ChangeMoodValue("Mood", _lit ? 0 : 1, _lit ? 1 : 0, _sound_transition_duration));
            StartCoroutine(ChangeMoodValue("GoalProximity", 1 - Mathf.Clamp01((Vector2.Distance(_player.transform.position, transform.position) - _sound_distance_min) / (_sound_distance_max - _sound_distance_min)), 0, _sound_transition_duration));

            _maze.SetActive(true);

            _interact_sound_emitter.Play();

            

            if (_particleSystem)
            {
                _particleSystem.SetActive(true);
                StartCoroutine(TempFollowPlayer());
            }
        }
    }
}
