using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public event Action<Task> OnTaskStarted;
    public event Action<Task> OnTaskStateChanged;
    public event Action<Task> OnTaskAdvanced;

    public static TaskManager Instance { get; private set; }

    private Dictionary<string, Task> taskIdDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple TaskManager's actice");
        }
            taskIdDictionary = CreateTaskIdToDictionary();
    }

    private Dictionary<string, Task> CreateTaskIdToDictionary()
    {
        TaskInfoSO[] tasks = Resources.LoadAll<TaskInfoSO>("Tasks");

        Dictionary<string, Task> returnDictionary = new Dictionary<string, Task>();
        foreach (TaskInfoSO taskInfo in tasks)
        {
            if (returnDictionary.ContainsKey(taskInfo.id))
            {
                Debug.LogError("Duplicate task ID's found");
            }

            returnDictionary.Add(taskInfo.id, new Task(taskInfo));
        }

        return returnDictionary;
    }

    private void Update()
    {
        foreach (Task task in taskIdDictionary.Values)
        {
            if (task.state == TaskState.CantStart) //Add a check to see if player has finished task prerequisites
            {
                ChangeTaskState(task.taskInfo.id, TaskState.NotStarted);
            }
        }
    }

    public void StartTask(string id)
    {
        Task task = GetTaskById(id);
        task.InstantiateCurrentTaskStep(transform);
        OnTaskStarted?.Invoke(task);
        ChangeTaskState(id, TaskState.Started);
    }

    public void AdvanceTask(string id)
    {
        Task task = GetTaskById(id);
        task.MoveToNextStep();
        if (task.CurrentStepExists())
        {
            OnTaskAdvanced?.Invoke(task);
            task.InstantiateCurrentTaskStep(transform);
        }
        else
        {
            FinishTask(id);
        }
    }

    public void FinishTask(string taskId)
    {
        ChangeTaskState(taskId, TaskState.Finished);
    }

    public int GetTaskStepIndex(string taskId)
    {
        Task task = GetTaskById(taskId);
        return task.GetCurrentStepIndex();
    }

    public void TriggerTaskStepCheck(string taskId, int taskStepIndex)
    {
        foreach (Transform child in transform)
        {
            if(child.TryGetComponent<TaskStep>(out TaskStep taskStep))
            {
                string childId;
                int childStepIndex;
                taskStep.GetTaskStepInfo(out childId, out childStepIndex);
                if(childId == taskId && childStepIndex == taskStepIndex)
                {   
                    taskStep.CheckToFinishTaskStep();
                }
            }
        }
    }

    private void ChangeTaskState(string taskId, TaskState newState)
    {
        Task task = GetTaskById(taskId);
        task.state = newState;
        OnTaskStateChanged?.Invoke(task);
        Debug.Log($"Switched task with id: {taskId} to state: {newState}");
    }

    private Task GetTaskById(string id)
    {
        Task task = taskIdDictionary[id];
        if (task == null)
        {
            Debug.LogError($"No task with id: {id}, found");
        }
        return task;
    }
}
