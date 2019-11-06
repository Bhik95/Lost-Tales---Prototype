using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbstractPuzzleCondition : MonoBehaviour
{
    public AbstractPuzzle Owner;
    public bool IsSolved { private set; get; }

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
