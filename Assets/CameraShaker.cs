using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float _trauma_level; //Linearly in [0;1]
    private float _shake;

    [SerializeField] private float _max_angle;
    [SerializeField] private float _max_offset_x;
    [SerializeField] private float _max_offset_y;

    [SerializeField] private float _seed = 382746.3472f;
    [SerializeField] private float _frequency = 5;
    [SerializeField] private float _cooldown_amount = 0.1f;


    private void Update()
    {
        _shake = _trauma_level * _trauma_level; //Tweening

        float phase = Time.time * _frequency;

        float angle = _max_angle * _shake * (2 * Mathf.PerlinNoise(_seed, phase) - 1);
        float offset_x = _max_offset_x * _shake * (2 * Mathf.PerlinNoise(_seed, phase + 1) - 1);
        float offset_y = _max_offset_y * _shake * (2 * Mathf.PerlinNoise(_seed, phase + 2) - 1);

        transform.localEulerAngles = new Vector3(0, 0, angle);
        transform.localPosition = new Vector3(offset_x, offset_y, 0);

        _trauma_level -= _cooldown_amount * Time.deltaTime;
        if (_trauma_level < 0)
            _trauma_level = 0;
    }

    public void AddTrauma(float deltaTrauma)
    {
        _trauma_level += deltaTrauma;
    }
}
