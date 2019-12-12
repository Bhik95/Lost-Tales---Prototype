using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartStoryBoard : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject _toTurnOff;
    private bool started= true;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        started = false;
    }

    void Update()
    {
        if (Input.anyKeyDown && !started)
        {
            _toTurnOff.SetActive(false);
            playableDirector.gameObject.SetActive(true);
            started = true;
        }
    }
}
