using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactible : MonoBehaviour
{

    protected bool _player_nearby = false;
    protected abstract void OnPlayerNearby();
    protected abstract void OnPlayerFar();
    protected abstract void OnActivate();

    protected virtual void Update()
    {
        if (_player_nearby && Input.GetButtonDown("Activate"))
        {
            OnActivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == StaticVariables.Tags.Player)
        {
            TempUI.Instance.ShowInteractText();
            _player_nearby = true;
            OnPlayerNearby();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TempUI.Instance.HideInteractText();
        if (collision.gameObject.tag == StaticVariables.Tags.Player)
        {
            _player_nearby = false;
            OnPlayerFar();
        }
    }
}
