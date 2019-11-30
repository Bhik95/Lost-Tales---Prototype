using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzle3_1CrystalToggle : Interactible
{

    [SerializeField] private FMODUnity.StudioEventEmitter _crystal_sound;
    [SerializeField] private GameObject _brazier;

    private bool _state;

    public bool State {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            _brazier.SetActive(State);
        }
    
    }

    protected override void OnActivate()
    {
        State = !State;

        if (_state)
        {
            _crystal_sound.Play();
        }
    }

    protected override void OnPlayerFar()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnPlayerNearby()
    {
        //throw new System.NotImplementedException();
    }
}
