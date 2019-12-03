using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectsOnDisable : MonoBehaviour
{
    [SerializeField] private GameObject[] _to_enable;

    private void OnDisable()
    {
        for (int i = 0; i < _to_enable.Length; i++)
        {
            _to_enable[i].SetActive(true);
        }
    }
}
