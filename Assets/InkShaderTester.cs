using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkShaderTester : MonoBehaviour
{
    public float Speed = 1.0f;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sharedMaterial.SetFloat("_Cutoff", Mathf.Abs(Time.time * Speed % 1f));
    }
}
