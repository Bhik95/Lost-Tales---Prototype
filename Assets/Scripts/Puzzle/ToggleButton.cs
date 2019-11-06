using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : ButtonCondition
{
    protected override void LanternEnter(PointAndClickLantern pLantern)
    {
        base.LanternEnter(pLantern);
        IsSolved = true;
        Solve();
        pLantern.ChangeTarget(transform);
    }

    protected override void LanternExit(PointAndClickLantern pLantern)
    {
        base.LanternExit(pLantern);
        IsSolved = false;
        Solve();
    }
}
