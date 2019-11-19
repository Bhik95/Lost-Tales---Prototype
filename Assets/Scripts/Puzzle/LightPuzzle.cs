using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : AbstractPuzzle
{

    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject EnterWall;

    [SerializeField] private GameObject Effect;
    protected override void SolvePuzzle()
    {
        base.SolvePuzzle();
        Wall.SetActive(false);
        if (Effect)
        {
            Effect.SetActive(true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnterWall.SetActive(true);
    }
}
