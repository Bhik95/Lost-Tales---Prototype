#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum PrimaryColor
{
    RED, BLUE, YELLOW
}

public class PrimaryColorCrystal : Interactible
{


    [SerializeField] private PrimaryColor _color;

    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _on_lit_sound;

    private void Start()
    {
        ColoredCrystalPuzzleManager.Instance.StateChanged += OnManagerStateChanged;
    }

    private void OnManagerStateChanged(PrimaryColor color, bool value)
    {
        if(color == _color)
            DisplayStatus(value);
    }

    protected override void OnActivate()
    {
        switch (_color)
        {
            case PrimaryColor.BLUE:
                ColoredCrystalPuzzleManager.Instance.ActiveBlue = !ColoredCrystalPuzzleManager.Instance.ActiveBlue;
                break;
            case PrimaryColor.RED:
                ColoredCrystalPuzzleManager.Instance.ActiveRed = !ColoredCrystalPuzzleManager.Instance.ActiveRed;
                break;
            case PrimaryColor.YELLOW:
                ColoredCrystalPuzzleManager.Instance.ActiveYellow = !ColoredCrystalPuzzleManager.Instance.ActiveYellow;
                break;
        }
    }

    private void DisplayStatus(bool status)
    {
        _brazier.SetActive(status);
        if (status)
        {
            _on_lit_sound.Play();
        }
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
