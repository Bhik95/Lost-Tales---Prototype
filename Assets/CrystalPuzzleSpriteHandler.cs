using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzleSpriteHandler : MonoBehaviour
{
    [SerializeField] private Sprite Activated;
    [SerializeField] private Sprite Deactivated;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetCrystalSprite(bool state)
    {
        if (state)
        {
            _renderer.sprite = Activated;
        }
        else
        {
            _renderer.sprite = Deactivated;
        }
    }
}
