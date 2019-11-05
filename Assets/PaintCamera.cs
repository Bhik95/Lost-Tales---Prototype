using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PaintCamera : MonoBehaviour
{
    private Camera _camera;
    private Texture _defaultTexture;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _defaultTexture = new Texture2D(8, 8);
        UpdateRenderTexture();
    }

    void UpdateRenderTexture()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            _camera.targetTexture = null;
            Shader.SetGlobalTexture("_CutoutTex", _defaultTexture);
        }
        else
        {
            _camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            _camera.targetTexture.SetGlobalShaderProperty("_CutoutTex");
        }
    }

}
