using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeBracer : Interactible
{

    [SerializeField] private GameObject[] _objects_to_deactivate;

    protected override void OnActivate()
    {
        foreach(GameObject go in _objects_to_deactivate)
        {
            go.SetActive(false);
        }
    }

    protected override void OnPlayerFar()
    {
        Debug.Log("TODO: Player is faraway (use to add juice)");
    }

    protected override void OnPlayerNearby()
    {
        Debug.Log("TODO: Player nearby  (use to add juice)");
    }
}
