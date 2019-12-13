using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
    [SerializeField] private CrystalObstacle _fade_in;
    [SerializeField] private float _camera_focus_duration = 0.3f;

    void Start()
    {
        _fade_in.AnimateThenSetActive(0.0f, true);
        Camera.main.transform.parent.GetComponent<CameraFollow>().SetTempTargetAndResetAfterTimeout(transform, _camera_focus_duration, true);
    }
}
