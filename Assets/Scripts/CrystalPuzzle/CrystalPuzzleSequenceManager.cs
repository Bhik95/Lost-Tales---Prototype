using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzleSequenceManager : MonoBehaviour
{

    [SerializeField] private CrystalPuzzleBrazier[] _crystals;
    [SerializeField] private GameObject[] gameobjects_to_disable;
    [SerializeField] private FMODUnity.StudioEventEmitter _correct_sound;
    [SerializeField] private FMODUnity.StudioEventEmitter _wrong_sound;
    private int _count = 0;
    private bool _correct = true;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _crystals.Length; i++)
        {
            _crystals[i].Activated += CrystalActivatedCallback;
        }
    }

    private void CrystalActivatedCallback(Interactible obj)
    {
        _correct = _correct && (obj.gameObject == _crystals[_count].gameObject);
        _count++;
        if(_count == _crystals.Length)
        {
            if (_correct)
            {
                OnSuccess();
            }
            else
            {
                OnFail();
            }

            _correct = true;
            _count = 0;
        }
    }

    private void OnFail()
    {
        //RESET the braziers
        for (int i = 0; i < _crystals.Length; i++)
        {
            _crystals[i].SetInteractible(true);
        }
        Debug.Log("WRONG -> RESET");

        _wrong_sound.Play();
    }

    private void OnSuccess()
    {
        //TODO CORRECT
        for(int i = 0; i < gameobjects_to_disable.Length; i++)
        {
            gameobjects_to_disable[i].SetActive(false);
        }

        Debug.Log("PUZZLE SOLVED");

        _correct_sound.Play();
    }


}
