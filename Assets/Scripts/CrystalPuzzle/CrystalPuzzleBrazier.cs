#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrystalPuzzleBrazier : Interactible
{

    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _crystal_sound;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private Transform ParticleSystemEffector;

    protected override void OnActivate()
    {
        _crystal_sound.Play();
        if (ParticleSystem)
            ParticleSystem.Play();
        SetInteractible(false);
    }

    public void SetInteractible(bool isInteractible)
    {
        StartCoroutine(SetInteractibleCoroutine(isInteractible));
    }

    private IEnumerator SetInteractibleCoroutine(bool isInteractible)
    {
        if (isInteractible)//If you want to make it interactible again you need to wait for the sound to end
        {
            while (_crystal_sound.IsPlaying())
            {
                yield return null;
            }
        }
        _brazier.SetActive(!isInteractible);
        this.enabled = isInteractible;
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
