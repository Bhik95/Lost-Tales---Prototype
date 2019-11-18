using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeFader : MonoBehaviour
{
    [SerializeField] private FMODUnity.StudioEventEmitter _emitter;
    
    [SerializeField] [Range(0, 1)] private float _sensitivity = 0.1f;

    private float _value = 0f;
    private float _target_value = 0f;

    private void Update()
    {
        _value = Mathf.Lerp(_value, _target_value, _sensitivity);
        _emitter.SetParameter(StaticVariables.FMODParameters.Volume, _value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == StaticVariables.Tags.Player)
        {
            _target_value = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == StaticVariables.Tags.Player)
        {
            _target_value = 0f;
        }
    }
}
