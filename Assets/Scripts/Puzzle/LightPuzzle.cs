using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : AbstractPuzzle
{
    [SerializeField] private Transform Center;

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
        Camera.main.transform.parent.GetComponent<CameraFollow>().TempTarget = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnterWall.SetActive(true);
        if (Center)
        {
            Camera.main.transform.parent.GetComponent<CameraFollow>().TempTarget = Center;
        }
    }

    public override void SolveCondition(AbstractPuzzleCondition pCondition = null)
    {
        base.SolveCondition(pCondition);
        if (pCondition == null)
        {
            return;
        }

        int index = Conditions.IndexOf(pCondition);
        int min = index - 1 >= 0 ? index - 1 : Conditions.Count - 1;
        int max = index + 1 < Conditions.Count ? index + 1 : 0;
        for (int i = 0; i < Conditions.Count; i++)
        {
            if (i == min || i == max || index == i)
            {
                if (Conditions[i].GetComponent<LightDarkBrazier>())
                {
                    Conditions[i].GetComponent<LightDarkBrazier>().Toggle();

                }
            }
        }
    }
}