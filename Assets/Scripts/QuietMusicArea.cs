using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuietMusicArea : MonoBehaviour
{
    public float DistanceMin => _distance_min;
    public float DistanceMax => _distance_max;
    [SerializeField] private float _distance_min;
    [SerializeField] private float _distance_max;

    private void Start()
    {
        MainMusic.Instance.AddQuietZone(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distance_min);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _distance_max);
    }

}
