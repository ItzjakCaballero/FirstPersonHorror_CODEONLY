using UnityEngine;

public enum TaskState
{
    CantStart,
    NotStarted,
    Started,
    Finished
}

public class Task
{
    public struct TaskTextInfo
    {
        public string taskName { get; private set; }
        public string taskObjective { get; private set; }
        public string stepDescription { get; private set; }
        public int stepIndex { get; private set; }
        public int totalSteps { get; private set; }

        public void SetTextInfo(string taskName, string taskObjective, string stepDescription, int stepIndex, int totalSteps)
        {
            this.taskName = taskName;
            this.taskObjective = taskObjective;
            this.stepDescription = stepDescription;
            this.stepIndex = stepIndex;
            this.totalSteps = totalSteps;
        }
    }

    public TaskInfoSO taskInfo;
    public TaskState state;

    private int currentStepIndex;
    private string currentStepDescription;

    public Task(TaskInfoSO taskInfo)
    {
        this.taskInfo = taskInfo;
        currentStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentStepIndex++;
    }

    public void InstantiateCurrentTaskStep(Transform parent)
    {
        GameObject taskStepPrefab = GetCurrentTaskStepPrefab();
        if (taskStepPrefab != null)
        {
            TaskStep taskStep = Object.Instantiate(taskStepPrefab, parent).GetComponent<TaskStep>();
            currentStepDescription = taskStep.taskStepDescription;
            taskStep.InitializeTaskStep(taskInfo.id, currentStepIndex);
        }
    }

    public bool CurrentStepExists()
    {
        return (currentStepIndex < taskInfo.taskStepPrefabs.Length);
    }

    public int GetCurrentStepIndex()
    {
        return currentStepIndex;
    }

    public TaskTextInfo GetTaskTextInfo()
    {
        TaskTextInfo taskTextInfo = new TaskTextInfo();
        taskTextInfo.SetTextInfo(taskInfo.taskName, taskInfo.taskObjective, currentStepDescription, currentStepIndex + 1, taskInfo.taskStepPrefabs.Length);
        return taskTextInfo;
    }

    private GameObject GetCurrentTaskStepPrefab()
    {
        GameObject taskStepPrefab = null;
        if(CurrentStepExists())
        {
            taskStepPrefab = taskInfo.taskStepPrefabs[currentStepIndex];
        }
        else
        {
            Debug.LogError("Current task step does not exist");
        }
        return taskStepPrefab;
    }
}
