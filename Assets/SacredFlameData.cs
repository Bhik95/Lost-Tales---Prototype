using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlameType
{
    GREEN=0, YELLOW=1, PURPLE=2
}

public class SacredFlameData : MonoBehaviour
{
    [SerializeField] private FlameType _flameType;
    public FlameType FlameType => _flameType;
}
