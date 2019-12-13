using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObstacle : MonoBehaviour
{
    [SerializeField] FMODUnity.StudioEventEmitter _vanishing_sound;
    [SerializeField] private CrystalToggleFXData _fx;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    
    public void AnimateThenSetActive(float delay, bool active)
    {
        StartCoroutine(AnimateThenSetActiveCoroutine(delay, active));
    }

    private IEnumerator AnimateThenSetActiveCoroutine(float delay, bool active)
    {
        yield return new WaitForSeconds(delay);
        _vanishing_sound.Play();
        if(_collider)
            _collider.enabled = active;
        _fx.StartCrystalAnimation();
        yield return new WaitForSeconds(_fx.Duration);
        gameObject.SetActive(active);
    }
}
