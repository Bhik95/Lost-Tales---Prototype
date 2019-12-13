using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : AbstractPuzzle
{
    [SerializeField] private FMODUnity.StudioEventEmitter SoundFinish;
    [SerializeField] private FMODUnity.StudioEventEmitter DissapearSound;

    [SerializeField] private Transform Center;

    [SerializeField] private GameObject Wall;
    [SerializeField] private CrystalObstacle WallCrystalObstacle;
    [SerializeField] private GameObject EnterWall;

    [SerializeField] private GameObject Effect;

    protected override void SolvePuzzle()
    {
        base.SolvePuzzle();
        var dtor = Wall.GetComponent<DelayedTurnOffRock>();
        if (dtor)
        {
            //PK's effect
            DissapearSound.Play();
            dtor.TurnOff();
        }
        else if (WallCrystalObstacle)
        {
            //Fra's effect (FadeOut like crystals)
            WallCrystalObstacle.AnimateThenSetActive(0.5f, false);
        }
        else
        {
            Wall.SetActive(false);

        }
        if (Effect)
        {
            Effect.SetActive(true);
        }
        SoundFinish.Play();
        StartCoroutine(DelayedSolve());
    }

    IEnumerator DelayedSolve()
    {
        yield return new WaitForSeconds(.5f);

        Camera.main.transform.parent.GetComponent<CameraFollow>().SetTempTargetAndResetAfterTimeout(Wall.transform, 2.0f, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!puzzleSolved)
        {
            if (!EnterWall.activeSelf)
            {
                EnterWall.SetActive(true);
            }
            if (Center)
            {
                Camera.main.transform.parent.GetComponent<CameraFollow>().TempTarget = Center;
            }
        }
       
    }

    public override void SolveCondition(AbstractPuzzleCondition pCondition = null)
    {
        base.SolveCondition(pCondition);
        if (pCondition == null)
        {
            return;
        }

        if (Conditions.Count > 3)
        {
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
}