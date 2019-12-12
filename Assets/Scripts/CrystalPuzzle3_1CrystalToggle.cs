using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzle3_1CrystalToggle : Interactible
{

    [SerializeField] private FMODUnity.StudioEventEmitter _crystal_sound;
    [SerializeField] private CrystalPuzzleSpriteHandler _sprite_handler;

    private CrystalToggleFXData _crystal_animation;

    private void Awake()
    {
        _crystal_animation = gameObject.GetComponentInParent<CrystalToggleFXData>();
    }

    private bool _state;

    public bool State {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            _sprite_handler.SetCrystalSprite(State);
        }
    
    }

    protected override void OnActivate()
    {
        State = !State;

        if (_state)
        {
            _crystal_animation.StartCrystalAnimation();
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
