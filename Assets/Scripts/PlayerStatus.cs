using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;
    GameObject FireParticleEffect;
    public bool HasBigFlame
    {
        get
        {
            return m_hasBigFlame;
        }
        set
        {
            if (FireParticleEffect)
            {
                FireParticleEffect.SetActive(value);
            }
            m_hasBigFlame = value;
        }
    }

    bool m_hasBigFlame;

    private void Awake()
    {
        Instance = this;
        //HasBigFlame = true;
    }
}
