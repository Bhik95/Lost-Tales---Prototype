using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMystAfterFlameCollection : MonoBehaviour
{
    [SerializeField] private ParticleSystem _myst;
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
        _myst.Stop();
    }
}
