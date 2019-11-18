#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CrystalPuzzlePlay : Interactible
{
    
    [System.Serializable]
    private class CrystalBrazierWithCorrectState
    {
        public CrystalPuzzleBrazierToggle Crystal;
        public bool State;
    }

    [SerializeField] private GameObject _brazier;
    [SerializeField] private CrystalBrazierWithCorrectState[] _crystalBraziersWithCorrectState;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private GameObject[] _gameobjects_to_disable;
    [SerializeField] private FMODUnity.StudioEventEmitter _correct_sound;
    [SerializeField] private FMODUnity.StudioEventEmitter _wrong_sound;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private Transform ParticleSystemEffector;

    protected override void OnActivate()
    {
        _brazier.SetActive(true);
        StartCoroutine(PlayCrystals());
        if (ParticleSystem)
            ParticleSystem.Play();
    }

    IEnumerator PlayCrystals()
    {
        this.enabled = false;
        float timer = 0;
        bool first = true;

        bool correct = true;

        /*Play all the crystal sounds in a sequence (delay is the time between one sound and the other*/
        for (int i = 0; i < _crystalBraziersWithCorrectState.Length; i++)
        {

            correct = correct && (_crystalBraziersWithCorrectState[i].Crystal.Active == _crystalBraziersWithCorrectState[i].State);

            if (!_crystalBraziersWithCorrectState[i].Crystal.Active)
                continue;

            timer = 0f;
            while (!first && timer <= delay)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            first = false;
            _crystalBraziersWithCorrectState[i].Crystal.PlayCrystalSound();
        }

        /*Wait for a little*/
        timer = 0f;
        while(timer <= delay)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        if (correct)
        {
            for (int i = 0; i < _gameobjects_to_disable.Length; i++)
                _gameobjects_to_disable[i].SetActive(false);
            _correct_sound.Play();
        }
        else
        {
            _wrong_sound.Play();
            /*Turn off the Brazier and make the brazier interactable again*/
            this.enabled = true;
            _brazier.SetActive(false);
        }


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
