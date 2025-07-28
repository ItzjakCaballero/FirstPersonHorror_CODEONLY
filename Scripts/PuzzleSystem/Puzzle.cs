using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] private string puzzleID;

    private bool isSolved = false;

    public void BaseCheckForSolutoin()
    {
        CheckForSolution();
    }

    protected virtual void CheckForSolution()
    {

    }

    protected void MarkAsSolved()
    {
        isSolved = true;
    }

    public bool GetIsSolved()
    {
        return isSolved;
    }
}
