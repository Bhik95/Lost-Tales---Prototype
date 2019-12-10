﻿#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrystalPuzzle3_2BrazierPlaySequenceWithLight : Interactible
{

    [SerializeField] private GameObject _brazier;
    [SerializeField] private CrystalPuzzle3_1CrystalToggle[] _crystals;
    [SerializeField] private float delay = 0.5f;//Time between each note

    protected override void OnActivate()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        enabled = false;
        _brazier.SetActive(true);
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
        enabled = true;
        _brazier.SetActive(false);
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