using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeout : MonoBehaviour
{
    [SerializeField] private float _timeout = 1f;
    private float _timer;

    void Start()
    {
        _timer = _timeout;
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer <= 0)
        {
            Destroy(gameObject);
        }
        _timer -= Time.deltaTime;
    }
}
