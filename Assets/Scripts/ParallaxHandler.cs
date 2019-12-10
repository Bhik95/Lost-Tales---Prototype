using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxHandler : MonoBehaviour
{

    [SerializeField] private RectTransform[] ParallaxElements;
    [SerializeField] private Image[] _fadingElements;
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _alpha = 1;

    void Update()
    {
        float z;
        for(int i = 0; i < ParallaxElements.Length; i++){
            z = ParallaxElements[i].localPosition.z;
            ParallaxElements[i].localPosition = new Vector3(_amplitude / -z * Mathf.Sin(_frequency * Time.time), 0, z);
        }
        float interval = _fadingElements.Length;
        for(int i = 0; i < _fadingElements.Length; i++)
        {
            float _alpha_i = Mathf.Clamp01(interval * _alpha - i);
            _fadingElements[i].color = new Color(1f, 1f, 1f, _alpha_i);
        }
    }
}
