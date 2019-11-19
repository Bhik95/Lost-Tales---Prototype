using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzleEnableOnTriggerEnter : MonoBehaviour
{

    [SerializeField] private GameObject[] _go_to_activate;
    private Collider2D _collider;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == StaticVariables.Tags.Player)
        {
            for (int i = 0; i < _go_to_activate.Length; i++)
                _go_to_activate[i].SetActive(true);

            Destroy(gameObject);
        }
    }
}
