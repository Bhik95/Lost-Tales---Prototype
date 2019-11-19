using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDarkBrazier : Interactible
{
    [SerializeField] AbstractPuzzleCondition Condition;
    [SerializeField] bool ShouldBeActive;


    [SerializeField] private GameObject[] _objects_to_activate;
    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _on_lit_sound;
    [SerializeField] private Transform ParticleSystemEffector;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        if (!ShouldBeActive)
        {
            Condition.Solve();
        }
    }
   


    protected override void OnActivate()
    {

        if (!_brazier.activeInHierarchy)
        {
            if (ShouldBeActive)
            {
                Condition.Solve();
            }
            else
            {
                Condition.Solve();
            }
            _brazier.SetActive(true);
            foreach (GameObject go in _objects_to_activate)
            {
                go.SetActive(true);
                if (ParticleSystemEffector)
                {
                    ParticleSystemEffector.position = go.transform.position;
                }
            }
            _on_lit_sound.Play();

            //this.enabled = false;
        }
        else
        {

            if (!ShouldBeActive)
            {
                Condition.Solve();
            }
            else
            {
                Condition.Solve();
            }
            _brazier.SetActive(false);
            foreach (GameObject go in _objects_to_activate)
            {
                go.SetActive(false);
                if (ParticleSystemEffector)
                {
                    ParticleSystemEffector.position = go.transform.position;
                }
            }
            _on_lit_sound.Play();

            //this.enabled = false;
        }

        
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
