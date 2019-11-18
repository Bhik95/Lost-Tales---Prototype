using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monobehaviour to play an fmod sound on interaction
/// </summary>
public class FModSoundOnInteract : Interactible
{

    [SerializeField] private FMODUnity.StudioEventEmitter _emitter;
    [SerializeField] private bool _deactivate_after_interaction = true;

    protected override void OnActivate()
    {
        _emitter?.Play();
        if (_deactivate_after_interaction)
            this.enabled = false;
    }

    protected override void OnPlayerFar()
    {
        
    }

    protected override void OnPlayerNearby()
    {
        
    }

}
