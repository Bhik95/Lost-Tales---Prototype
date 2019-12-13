#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrystalPuzzle3_1BrazierPlaySequence : Interactible
{

    [SerializeField] private FMODUnity.StudioEventEmitter[] _crystal_sound_sequence;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private FMODUnity.StudioEventEmitter _fire_sound;
    [SerializeField] private float delay = 0.5f;//Time between each note

    protected override void OnActivate()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        enabled = false;
        PlayerStatus.Instance.GetComponent<Movement>().ControlsEnabled = false;
        float timer;
        if (ParticleSystem)
            ParticleSystem.Play();
        _fire_sound.Play();
        for (int i = 0; i < _crystal_sound_sequence.Length; i++)
        {
            _crystal_sound_sequence[i].Play();
            timer = delay;
            while(timer > 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
        }
        enabled = true;
        PlayerStatus.Instance.GetComponent<Movement>().ControlsEnabled = true;
        if (ParticleSystem)
            ParticleSystem.Stop();
        _fire_sound.Stop();
    }


    protected override void OnPlayerFar()
    {
        //Debug.Log("TODO: Player is faraway (use to add juice)");
    }

    protected override void OnPlayerNearby()
    {
        //Debug.Log("TODO: Player nearby  (use to add juice)");
    }
}
