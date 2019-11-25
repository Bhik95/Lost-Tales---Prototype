using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ButtonCondition : AbstractPuzzleCondition
{
    protected bool LanternOnButton;
    protected Lantern Lantern;
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void UpdateCondition()
    {
        base.UpdateCondition();
    }

    public override void Solve(bool useThis = false)
    {
        base.Solve(useThis);
    }


    protected virtual void LanternEnter(PointAndClickLantern pLantern)
    {

    }

    protected virtual void LanternExit(PointAndClickLantern pLantern)
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == StaticVariables.Layers.Lantern)
        {
            LanternOnButton = true;
            LanternEnter(col.GetComponent<PointAndClickLantern>());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == StaticVariables.Layers.Lantern)
        {
            LanternOnButton = true;
            LanternExit(col.GetComponent<PointAndClickLantern>());
        }
    }
}
