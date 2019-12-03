#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarMusicChange : Interactible
{
    [Header("Music")]
    [SerializeField] private FMODUnity.StudioEventEmitter _interact_sound_emitter;
    public float SoundDistanceMin => _sound_distance_min;
    public float SoundDistanceMax => _sound_distance_max;
    [SerializeField] private float _sound_distance_min;
    [SerializeField] private float _sound_distance_max;
    [SerializeField] private float _sound_transition_duration = 1f;

    [Header("GameObjects")]
    [SerializeField] private GameObject _Light;
    [SerializeField] private GameObject _maze;
    [SerializeField] private GameObject _disableGO;

    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private GameObject _particleSystemEffector;
    [SerializeField] private GameObject _flameFXPlayer;

    [Header("CameraShake")]
    [SerializeField] private float _camera_shake_trauma = 0.2f;
    [SerializeField] private float _camera_shake_trauma_after = 0.3f;


    private bool _lit = false;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        MainMusic.Instance.AddAltar(this);
    }

    protected override void Update()
    {
        base.Update();
    }

    

    private IEnumerator TempFollowPlayer()
    {
        float timer = 0;
        while (timer < 1)
        {
            if (_Light)
            {
                _Light.transform.localScale = Vector3.MoveTowards(_Light.transform.localScale, new Vector3(1.2f,1.2f,1),Time.deltaTime);
            }

            timer += Time.deltaTime;
            _particleSystemEffector.transform.position = _player.transform.position;
            yield return null;
        }
        Camera.main.GetComponent<CameraShaker>().AddTrauma(_camera_shake_trauma_after);

        GameObject flameFXPlayerInstance = Instantiate(_flameFXPlayer, GameObject.FindWithTag(StaticVariables.Tags.Player).transform);
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

            _maze?.SetActive(true);
            if (_disableGO)
            {
                _disableGO.SetActive(false);
            }
            
            _interact_sound_emitter.Play();


            PlayerStatus.Instance.HasBigFlame = true;
            if (_particleSystem)
            {
                _particleSystem.SetActive(true);
                StartCoroutine(TempFollowPlayer());
            }
        }
    }
}
