using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplementaryColorCrystal : MonoBehaviour
{

    [Header("Color Components")]
    [SerializeField] private bool _red;
    [SerializeField] private bool _blue;
    [SerializeField] private bool _yellow;

    private void Start()
    {
        ColoredCrystalPuzzleManager.Instance.StateChanged += OnManagerStateChanged;
    }

    private void OnManagerStateChanged(PrimaryColor color, bool value)
    {
        int c = 0;
        if (_red && ColoredCrystalPuzzleManager.Instance.ActiveRed)
            c++;
        if (_blue && ColoredCrystalPuzzleManager.Instance.ActiveBlue)
            c++;
        if (_yellow && ColoredCrystalPuzzleManager.Instance.ActiveYellow)
            c++;

        gameObject.SetActive(c < 2);
    }

    private void OnDestroy()
    {
        ColoredCrystalPuzzleManager.Instance.StateChanged -= OnManagerStateChanged;
    }

}
