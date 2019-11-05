using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchicSpawner : MonoBehaviour
{

    public GameObject TorchicPrefab;
    public int N = 3;
    public float Distance = 1.0f;

    private void Start()
    {
        //GenerateTorchics();
    }

    [ContextMenu("Create Torchics")]
    private void GenerateTorchics()
    {
        float offset = N % 2 == 0 ? -.5f : 0f;
        float i_norm = -(N / 2.0f);
        GameObject go;
        float shader_offset;
        for (int i = 0; i < N; i++)
        {
            i_norm++;
            go = Instantiate(TorchicPrefab, transform.position + (offset + i_norm * Distance) * Vector3.right, Quaternion.identity, transform);
            go.name = "Torchic " + i;
            shader_offset = N > 1 ? i / (float)(N - 1) : 0.5f;
            go.GetComponent<Renderer>().material.SetFloat("_Offset", shader_offset);

        }
    }
}
