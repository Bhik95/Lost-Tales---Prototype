using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzle3_2ClosePathOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _game_object_to_reactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(StaticVariables.Tags.Player))
            _game_object_to_reactivate.SetActive(true);
    }
}
