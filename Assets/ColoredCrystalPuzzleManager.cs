using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredCrystalPuzzleManager : MonoBehaviour
{
    private bool _active_red = false;
    private bool _active_yellow = false;
    private bool _active_blue = false;

    public event Action<PrimaryColor, bool> StateChanged;

    public static ColoredCrystalPuzzleManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<ColoredCrystalPuzzleManager>();

            return _instance;
        }

}

    private static ColoredCrystalPuzzleManager _instance;

    private void Awake()
    {
        if (_instance)
        {
            Debug.LogError("Singleton violated.");
            return;
        }

        _instance = this;
    }

    public bool ActiveRed
    {
        get
        {
            return _active_red;
        }
        set
        {
            _active_red = value;
            StateChanged?.Invoke(PrimaryColor.RED, value);
        }
    }
    
    public bool ActiveBlue
    {
        get
        {
            return _active_blue;
        }
        set
        {
            _active_blue = value;
            StateChanged?.Invoke(PrimaryColor.BLUE, value);
        }
    }
    
    public bool ActiveYellow
    {
        get
        {
            return _active_yellow;
        }
        set
        {
            _active_yellow = value;
            StateChanged?.Invoke(PrimaryColor.YELLOW, value);
        }
    }
    
}
