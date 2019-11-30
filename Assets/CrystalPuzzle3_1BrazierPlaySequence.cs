#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrystalPuzzle3_1BrazierPlaySequence : Interactible
{

    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter[] _crystal_sound_sequence;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private float delay = 0.5f;//Time between each note

    protected override void OnActivate()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        enabled = false;
        _brazier.SetActive(true);
        float timer;
        if (ParticleSystem)
            ParticleSystem.Play();
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
        _brazier.SetActive(false);
        if (ParticleSystem)
            ParticleSystem.Stop();
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
