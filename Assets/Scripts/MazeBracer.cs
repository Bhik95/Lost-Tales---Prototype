#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeBracer : Interactible
{

    [SerializeField] private GameObject[] _objects_to_deactivate;
    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _on_lit_sound;
    [SerializeField] private Transform ParticleSystemEffector;


    protected override void OnActivate()
    {
        _brazier.SetActive(true);
        foreach(GameObject go in _objects_to_deactivate)
        {
            go.SetActive(false);
            ParticleSystemEffector.position = go.transform.position;
        }
        _on_lit_sound.Play();
        this.enabled = false;
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
