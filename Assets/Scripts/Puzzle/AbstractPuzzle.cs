using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPuzzle : MonoBehaviour
{
    protected bool puzzleSolved;
    [SerializeField]protected List<AbstractPuzzleCondition> Conditions;
    protected virtual void Initialize() { }
    protected virtual void UpdatePuzzle() { }

    protected virtual void SolvePuzzle() { puzzleSolved = true; }
    protected virtual void UnsolvePuzzle() { puzzleSolved = false; }
    public virtual void SolveCondition()
    {
        if (CheckIfSolved() && !puzzleSolved)
        {
            SolvePuzzle();
        }
        else if(puzzleSolved)
        {
            UnsolvePuzzle();
        }
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
