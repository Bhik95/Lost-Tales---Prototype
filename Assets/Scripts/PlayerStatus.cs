using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;
    public bool HasBigFlame
    {
        set
        {
            _hasBigFlame = value;
            if (_hasBigFlame)
                HadBigFlame = true;
        }
        get
        {
            return _hasBigFlame;
        }
    }
    [SerializeField] private bool _debug_has_big_flame = false;
    private bool _hasBigFlame;
    public bool HadBigFlame { get; private set; }
    public Vector3 flameSize;
    private void Awake()
    {
        Instance = this;
        //HasBigFlame = true;
    }

    private void Start()
    {
        if (_debug_has_big_flame)
        {
            Debug.LogWarning("Did you forget DebugHasHigFlame=true, put it to FALSE before you build!");
            HasBigFlame = true;
        }
    }

    [ContextMenu("DEBUG Set HasBigFlame = true")]
    private void DebugCommandSetBigFlame()
    {
        HasBigFlame = true;
        Debug.Log("HasBigFlame set to " + HasBigFlame);
    }
}
