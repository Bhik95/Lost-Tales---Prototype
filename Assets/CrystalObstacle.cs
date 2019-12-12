using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObstacle : MonoBehaviour
{
    [SerializeField] FMODUnity.StudioEventEmitter _vanishing_sound;
    private CrystalToggleFXData _fx;
    private Collider2D _collider;

    private void Awake()
    {
        _fx = GetComponent<CrystalToggleFXData>();
        _collider = GetComponent<Collider2D>();
    }
    
    public void AnimateThenDeactivate(float delay)
    {
        StartCoroutine(AnimateThenDeactivateCoroutine(delay));
    }

    private IEnumerator AnimateThenDeactivateCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _vanishing_sound.Play();
        _collider.enabled = false;
        _fx.StartCrystalAnimation();
        yield return new WaitForSeconds(_fx.Duration);
        gameObject.SetActive(false);
    }
}
