using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractPuzzle : MonoBehaviour
{
    protected bool puzzleSolved;
    [SerializeField]protected List<AbstractPuzzleCondition> Conditions;
    protected virtual void Initialize() { }
    protected virtual void UpdatePuzzle() { }

    protected virtual void SolvePuzzle() { puzzleSolved = true; }
    protected virtual void UnsolvePuzzle() { puzzleSolved = false; }

    public virtual void SolveCondition(AbstractPuzzleCondition pCondition)
    {
        if (CheckIfSolved() && !puzzleSolved)
        {
            SolvePuzzle();
        }
        else if(puzzleSolved)
        {
            UnsolvePuzzle();
        }

        //int index = Conditions.IndexOf(pCondition);
        //for (int i = 0; i < Conditions.Count; i++)
        //{
        //    if (i < index+1 && i > index-1)
        //    {

        //    }
        //}
    }

    protected virtual bool CheckIfSolved()
    {
        foreach (var item in Conditions)
        {
            if (!item.IsSolved)
            {
                return false;
            }
        }
        return true;
    }

    void Start()
    {
        Initialize();
        foreach (var item in Conditions)
        {
            item.Owner = this;
            item.Initialize();
        }
    }

    void Update()
    {
        UpdatePuzzle();
        foreach (var item in Conditions)
        {
            item.UpdateCondition();
        }
    }
}
