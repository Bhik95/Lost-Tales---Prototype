#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeBrazier : Interactible
{

    [SerializeField] private GameObject[] _objects_to_deactivate;
    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _on_lit_sound;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private Transform ParticleSystemEffector;


    protected override void OnActivate()
    {
        _brazier.SetActive(true);
        foreach(GameObject go in _objects_to_deactivate)
        {
            go.SetActive(false);
            if (ParticleSystemEffector)
            {
                ParticleSystemEffector.position = go.transform.position;
                StartCoroutine(MoveCamera(go.transform));
            }
        }
        _on_lit_sound.Play();
     
        if(ParticleSystem)
            ParticleSystem.Play();
        this.enabled = false;
    }

    IEnumerator MoveCamera(Transform trans)
    {
        Camera.main.transform.parent.GetComponent<CameraFollow>().TempTarget = trans;
        yield return new WaitForSeconds(1f);
        Camera.main.transform.parent.GetComponent<CameraFollow>().TempTarget = null;

    }

    protected override void OnPlayerFar()
    {
        //Debug.Log("TODO: Player is faraway (use to add juice)");
    }

    protected override void OnPlayerNearby()
    {
        //Debug.Log("TODO: Player nearby  (use to add juice)");
    }
}
