using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnButtonToggle : AbstractPuzzle
{
    [SerializeField]List<GameObject> ObjectsToEnable;
    [SerializeField]List<ParticleSystem> ParticlesToPlayOnEnableDisable;


    protected override void SolvePuzzle()
    {
        base.SolvePuzzle();
        ObjectsToEnable.ForEach(a => a.SetActive(true));
        ParticlesToPlayOnEnableDisable.ForEach(a => a.Play());
    }

    protected override void UnsolvePuzzle()
    {
        base.UnsolvePuzzle();
        ObjectsToEnable.ForEach(a => a.SetActive(false));
        ParticlesToPlayOnEnableDisable.ForEach(a => a.Play());
    }
}
