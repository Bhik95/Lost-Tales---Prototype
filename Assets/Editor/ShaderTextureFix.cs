using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShaderTextureFix : MonoBehaviour
{
    [MenuItem("ColorDisplay/Show Full Colors")]
    static void ShowFullColors()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        Shader.SetGlobalTexture("_CutoutTex", texture);
        Debug.Log("Show full colors");
    }

    [MenuItem("ColorDisplay/Show Default Desaturation")]
    static void ShowDesaturation()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.black);
        texture.Apply();
        Shader.SetGlobalTexture("_CutoutTex", texture);
        Debug.Log("Show default desaturated colors");
    }
}
