using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactible
{
    [SerializeField] private GameObject _paintSprite;
    [SerializeField] private GameObject _brazier;
    [SerializeField] private FMODUnity.StudioEventEmitter _on_lit_sound;
    protected override void OnActivate()
    {
        _brazier.SetActive(true);
        _on_lit_sound.Play();
        if (PlayerStatus.Instance.HadBigFlame)
        {
            _paintSprite.transform.localScale = new Vector3(0.7f,0.7f,1);
        }
        else
        {
            _paintSprite.transform.localScale = new Vector3(.2f, .2f, 1);
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
