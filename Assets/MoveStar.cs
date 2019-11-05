using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStar : MonoBehaviour
{

    public Sprite StarSprite;
    public Sprite SmoothSprite;

    private bool _star = false;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPosition.z = 0f;
        transform.position = finalPosition;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _star = !_star;
            _renderer.sprite = _star ? StarSprite : SmoothSprite;
            _renderer.material.SetTexture("_MainTex", _star ? StarSprite.texture : SmoothSprite.texture);
        }
    }
}
