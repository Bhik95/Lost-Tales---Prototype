#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrystalPuzzleBrazierToggle : Interactible
{

    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _crystal_sound;
    [SerializeField] private FMODUnity.StudioEventEmitter _on_lit_sound;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private Transform ParticleSystemEffector;
    [SerializeField] private bool _active = false;

    public bool Active => _active;
    public void PlayCrystalSound()
    {
        _crystal_sound.Play();
    }

    protected override void OnActivate()
    {
        _active = !_active;

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _brazier.SetActive(_active);

        if (_active)
            _on_lit_sound.Play();

        if (ParticleSystem)
        {
            if (_active)
                ParticleSystem.Play();
            else
                ParticleSystem.Stop();
        }
    }


    protected override void OnPlayerFar()
    {
        //Debug.Log("TODO: Player is faraway (use to add juice)");
    }

    protected override void OnPlayerNearby()
    {
        //Debug.Log("TODO: Player nearby  (use to add juice)");
    }

    private void OnValidate()
    {
        UpdateStatus();
    }
}
