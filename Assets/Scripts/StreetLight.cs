using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _light_go;

    public float Cooldown = 3.0f;
    private float _timer = 0f;
    private bool _lit = false;

    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_timer <= 0 && collision.gameObject.layer == LayerMask.NameToLayer("Lantern"))
        {
            _lit = !_lit;

            _timer = Cooldown;

            _light_go.SetActive(_lit);
            _animator.SetBool("Lit", _lit);
        }
    }
}
