using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzle3_1Manager : MonoBehaviour
{
    [SerializeField] private CrystalPuzzle3_1CrystalToggle[] _crystal_sequence;

    [SerializeField] private GameObject[] _obstacles_to_remove;
    [SerializeField] private FMODUnity.StudioEventEmitter _success_sound;
    [SerializeField] private FMODUnity.StudioEventEmitter _fail_sound;

    private int _counter = 1;
    private int[] _current_order;

    private void OnEnable()
    {
        _current_order = new int[_crystal_sequence.Length];

        for (int i = 0; i < _crystal_sequence.Length; i++)
        {
            _crystal_sequence[i].Activated += OnCrystalActivated;
        }
    }

    private void OnDisable()
    {
        {
            for (int i = 0; i < _crystal_sequence.Length; i++)
            {
                _crystal_sequence[i].Activated -= OnCrystalActivated;
            }
        }
    }

    private void OnCrystalActivated(Interactible obj)
    {
        int index = Array.IndexOf(_crystal_sequence, obj);

        if (_crystal_sequence[index].State)
        {
            //Crystal activated

            _current_order[index] = _counter;

            _counter++;
        }
        else
        {
            //Crystal deactivated

            int old_counter = _current_order[index];
            _current_order[index] = 0;

            for(int i = 0; i < _current_order.Length; i++)
            {
                if (_current_order[i] > old_counter)
                    _current_order[i]--;
            }

            _counter--;
        }

        CheckIfPuzzleSolved();
    }

    private void CheckIfPuzzleSolved()
    {
        if (_counter > _current_order.Length)
        {
            bool solved = true;
            for(int i = 0; i < _current_order.Length; i++)
            {
                if(_crystal_sequence[i] != _crystal_sequence[_current_order[i] - 1])
                {
                    solved = false;
                    break;
                }
            }

            if (solved)
            {
                for(int i = 0; i < _obstacles_to_remove.Length; i++)
                {
                    _obstacles_to_remove[i].SetActive(false);
                }
                _success_sound.Play();
                enabled = false;
            }
            else
            {
                _fail_sound.Play();
            }
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        _counter = 1;
        for(int i = 0; i < _current_order.Length; i++)
        {
            _current_order[i] = 0;
            _crystal_sequence[i].State = false;
        }
    }
}
