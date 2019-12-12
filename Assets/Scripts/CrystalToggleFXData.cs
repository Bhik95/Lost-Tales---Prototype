using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalToggleFXData : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [Header("Displacement (C)")]
    [SerializeField] private float _c_start = -3.0f;
    [SerializeField] private float _c_end = 3.0f;
    [SerializeField] private AnimationCurve _tweening_c ;
    [Header("Intensity")]
    [SerializeField] private float _intensity_start = 1.0f;
    [SerializeField] private float _intensity_end = 0.0f;
    [SerializeField] private AnimationCurve _tweening_intensity;

    private SpriteRenderer _renderer;
    private bool _running = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator AnimateCrystalFX()
    {
        _running = true;

        float timer = 0;
        while(timer < _duration)
        {
            float t = timer / _duration;
            _renderer.material.SetFloat("_C", Mathf.Lerp(_c_start, _c_end, _tweening_c.Evaluate(t)));
            _renderer.material.SetFloat("_Intensity", Mathf.Lerp(_intensity_start, _intensity_end, _tweening_intensity.Evaluate(t)));
            timer += Time.deltaTime;
            yield return null;
        }
        _renderer.material.SetFloat("_C", _c_end);
        _renderer.material.SetFloat("_Intensity", _intensity_end);

        _running = false;
    }

    public void StartCrystalAnimation()
    {
        if(!_running)
            StartCoroutine(AnimateCrystalFX());
    }
}
