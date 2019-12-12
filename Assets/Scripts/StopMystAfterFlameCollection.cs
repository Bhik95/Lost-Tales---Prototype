using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMystAfterFlameCollection : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _myst_particle_systems;
    private Bonfire _bonfire;

    private void Awake()
    {
        _bonfire = GetComponent<Bonfire>();
    }
    private void OnEnable()
    {
        _bonfire.OnThreeFlamesCollected += OnThreeFlamesCollectedHandler;
    }

    private void OnDisable()
    {
        _bonfire.OnThreeFlamesCollected -= OnThreeFlamesCollectedHandler;
    }

    private void OnThreeFlamesCollectedHandler()
    {
        for(int i = 0; i < _myst_particle_systems.Length; i++)
        {
            _myst_particle_systems[i].Stop();
        }
    }
}
