using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;
    public bool HasBigFlame;
    public Vector3 flameSize;
    private void Awake()
    {
        Instance = this;
        //HasBigFlame = true;
    }
}
