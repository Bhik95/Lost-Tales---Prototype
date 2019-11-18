#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CrystalPuzzlePlayBrazier : Interactible
{

    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter[] _crystal_sounds;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private Transform ParticleSystemEffector;

    protected override void OnActivate()
    {
        _brazier.SetActive(true);
        StartCoroutine(PlayCrystals());
        if (ParticleSystem)
            ParticleSystem.Play();
        this.enabled = false;
    }

    IEnumerator PlayCrystals()
    {
        float timer = 0;
        /*Play all the crystal sounds in a sequence (delay is the time between one sound and the other*/
        for(int i=0; i< _crystal_sounds.Length; i++)
        {
            timer = 0f;
            while(i != 0 && timer <= delay)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            _crystal_sounds[i].Play();
        }
        /*Wait for the last sound to finish*/
        while(_crystal_sounds[_crystal_sounds.Length - 1].IsPlaying())
        {
            yield return null;
        }

        /*Turn off the Brazier and make the brazier interactable again*/
        this.enabled = true;
        _brazier.SetActive(false);

        yield return null;
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
