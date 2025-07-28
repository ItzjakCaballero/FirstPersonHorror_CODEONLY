using UnityEngine;

public class TaskStarter : MonoBehaviour
{
    [SerializeField] private TaskInfoSO taskInfo;

    private string taskId;
    private TaskState currentTaskState;

    private void Awake()
    {
        taskId = taskInfo.id;
    }

    private void Start()
    {
        TaskManager.Instance.OnTaskStateChanged += TaskManager_OnTaskStateChanged;
    }

    private void TaskManager_OnTaskStateChanged(Task task)
    {
        if (task.taskInfo.id == taskId)
        {
            currentTaskState = task.state;
        }
    }

    public void StartTask()
    {
        if (currentTaskState == TaskState.NotStarted)
        {
            TaskManager.Instance.StartTask(taskId);
        }
    }
}
