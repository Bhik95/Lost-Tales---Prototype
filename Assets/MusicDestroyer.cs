using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDestroyer : MonoBehaviour
{
    void Start()
    {
        Destroy(MainMusic.Instance.gameObject);
    }

}
