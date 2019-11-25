using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbstractPuzzleCondition : MonoBehaviour
{
    [HideInInspector]public AbstractPuzzle Owner;
    public bool IsSolved;// { protected set; get; }

    public virtual void Solve(bool useThis = false)
    {
        IsSolved = true;
        if (useThis)
        {
            Owner.SolveCondition(this);

        }
        else
        {
            Owner.SolveCondition();

        }
    }
    public virtual void UnSolve(bool useThis = false)
    {
        IsSolved = false;
        if (useThis)
        {
            Owner.SolveCondition(this);

        }
        else
        {
            Owner.SolveCondition();

        }
    }
    public virtual void Initialize()
    {

    }

    public virtual void UpdateCondition()
    {

    }
}
