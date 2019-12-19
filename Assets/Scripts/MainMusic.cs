using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{

    private static MainMusic _instance;

    public static MainMusic Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<MainMusic>();
            return _instance;
        }
    }

    private AltarMusicChange[] altars;
    private QuietMusicArea[] quietMusicAreas;
    [SerializeField] private FMODUnity.StudioEventEmitter _music;
    [SerializeField] private float _default_quiet_volume = 0.75f;
    [SerializeField] private bool _force_choir = false;

    public bool ForceChoir {
        get { return _force_choir; }
        set
        {
            _force_choir = value;
        }
    }


    private void Awake()
    {
        if (_instance && _instance != this)
        {
            Debug.LogWarning("Music was already found on the scene, I will self-destruct, bye!", this);
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (_force_choir)
        {
            _music.SetParameter("GoalProximity", 1);
        }
        if (PlayerStatus.Instance)
        {
            Transform playerTransform = PlayerStatus.Instance.transform;

            float goalProximity = float.PositiveInfinity;
            
            if(!_force_choir)
            {
                if (altars != null)
                {
                    for (int i = 0; i < altars.Length; i++)
                    {
                        if (!altars[i].enabled)
                            continue;
                        float goalProximityTemp = Mathf.Clamp01((Vector2.Distance(playerTransform.position, altars[i].transform.position) - altars[i].SoundDistanceMin) / (altars[i].SoundDistanceMax - altars[i].SoundDistanceMin));
                        goalProximity = Mathf.Min(goalProximity, goalProximityTemp);
                    }

                    _music.SetParameter("GoalProximity", 1 - goalProximity);
                }
            } 

            float quietZoneProximity = float.PositiveInfinity;
            if (quietMusicAreas != null)
            {
                for (int i = 0; i < quietMusicAreas.Length; i++)
                {
                    float quietZoneProximityTemp = Mathf.Clamp01((Vector2.Distance(playerTransform.position, quietMusicAreas[i].transform.position) - quietMusicAreas[i].DistanceMin) / (quietMusicAreas[i].DistanceMax - quietMusicAreas[i].DistanceMin));
                    quietZoneProximity = Mathf.Min(quietZoneProximity, quietZoneProximityTemp);
                }

                _music.SetParameter("Volume", (1 - quietZoneProximity) * _default_quiet_volume + quietZoneProximity);
            }

            //Type of music: PIANO on right side of the map (x>-11.85), HARPIANO on left side
            _music.SetParameter("Mood", Mathf.SmoothStep(0.1f, 0.9f, transform.position.x - playerTransform.position.x));

        } 
    }



    public IEnumerator ChangeParameterValue(string paramName, float startValue, float finalValue, float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            _music.SetParameter(paramName, Mathf.Lerp(startValue, finalValue, timer / duration));
            yield return null;
        }
        _music.SetParameter(paramName, finalValue);
    }

    public void AddAltar(AltarMusicChange altarMusicChange)
    {
        if(altars == null || altars.Length == 0)
        {
            altars = new AltarMusicChange[1];
            altars[0] = altarMusicChange;
        }
        else
        {
            var altarsN = new AltarMusicChange[altars.Length + 1];
            Array.Copy(altars, altarsN, altars.Length);
            altarsN[altars.Length] = altarMusicChange;
            altars = altarsN;
        }
    }

    public void AddQuietZone(QuietMusicArea quietMusicArea)
    {
        if (quietMusicAreas == null || quietMusicAreas.Length == 0)
        {
            quietMusicAreas = new QuietMusicArea[1];
            quietMusicAreas[0] = quietMusicArea;
        }
        else
        {
            var qmaN = new QuietMusicArea[quietMusicAreas.Length + 1];
            Array.Copy(quietMusicAreas, qmaN, quietMusicAreas.Length);
            qmaN[quietMusicAreas.Length] = quietMusicArea;
            quietMusicAreas = qmaN;
        }
    }
}
