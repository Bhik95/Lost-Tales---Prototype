using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuietMusicArea : MonoBehaviour
{
    [SerializeField] private float _quiet_music_volume;
    [SerializeField] private float _duration;

    private float _default_music_volume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(StaticVariables.Tags.Player))
        {
            _default_music_volume = MainMusic.Instance.GetMusicVolume();

            Debug.Log(_default_music_volume);

            MainMusic.Instance.ChangeMusicVolume(_quiet_music_volume, _duration);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(StaticVariables.Tags.Player))
        {
            MainMusic.Instance.ChangeMusicVolume(_default_music_volume, 1f);
        }
    }
}
