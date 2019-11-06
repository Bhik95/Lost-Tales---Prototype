using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbstractPuzzleCondition : MonoBehaviour
{
    [HideInInspector]public AbstractPuzzle Owner;
    public bool IsSolved { protected set; get; }

    protected virtual void Solve()
    {
        IsSolved = true;
        Owner.SolveCondition();
    }

    public virtual void Initialize()
    {

    }

    public virtual void UpdateCondition()
    {

    }
}
