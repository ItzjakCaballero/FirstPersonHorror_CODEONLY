using System;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    [field:SerializeField] public string taskStepDescription { get; private set; }

    private string taskID;
    private int stepIndex;

    public void InitializeTaskStep(string taskID, int stepIndex)
    {
        this.taskID = taskID;
        this.stepIndex = stepIndex;
    }

    public virtual void CheckToFinishTaskStep()
    {

    }

    protected void FinishTaskStep()
    {
        TaskManager.Instance.AdvanceTask(taskID);
        Destroy(gameObject);
    }

    public void GetTaskStepInfo(out string taskId, out int stepIndex)
    {
        taskId = taskID;
        stepIndex = this.stepIndex;
    }
}
