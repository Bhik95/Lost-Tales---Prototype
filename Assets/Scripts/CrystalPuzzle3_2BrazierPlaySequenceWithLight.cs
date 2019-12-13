#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrystalPuzzle3_2BrazierPlaySequenceWithLight : Interactible
{

    [SerializeField] private ParticleSystem _particle_system;
    [SerializeField] private FMODUnity.StudioEventEmitter _fire_sound;
    [SerializeField] private CrystalPuzzle3_1CrystalToggle[] _crystals;
    [SerializeField] private CrystalPuzzle3_1CrystalToggle[] _crystals_not_involved;
    [SerializeField] private float delay = 0.5f;//Time between each note

    protected override void OnActivate()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        enabled = false;
        PlayerStatus.Instance.GetComponent<Movement>().ControlsEnabled = false;
        if (_particle_system)
            _particle_system.Play();
        _fire_sound.Play();
        float timer;
        for (int i = 0; i < _crystals.Length; i++)
        {
            _crystals[i].GetComponentInChildren<FMODUnity.StudioEventEmitter>().Play();
            _crystals[i].State = true;
            timer = delay;
            while (timer > 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
            _crystals[i].State = false;
        }
        if (_crystals_not_involved != null)
        {
            for (int i = 0; i < _crystals_not_involved.Length; i++)
            {
                _crystals_not_involved[i].State = false;
            }
        }
        
        enabled = true;
        PlayerStatus.Instance.GetComponent<Movement>().ControlsEnabled = true;
        if (_particle_system)
            _particle_system.Stop();
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
