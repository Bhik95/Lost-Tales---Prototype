using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ButtonCondition : AbstractPuzzleCondition
{
    protected bool LanternOnButton;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void UpdateCondition()
    {
        base.UpdateCondition();
    }

    protected override void Solve()
    {
        base.Solve();
    }

    void OnTriggerEnter(Collider2D col)
    {
        if (col.gameObject.layer == StaticVariables.Layers.Default)
        {

        }
    }
}
