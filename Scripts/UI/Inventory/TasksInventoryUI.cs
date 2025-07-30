using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TasksInventoryUI : MonoBehaviour
{
    private struct TaskUIContainer
    {
        public string taskId;
        public Button button;
    }

    [Header("Text Parameters")]
    [SerializeField] private TextMeshProUGUI taskNameText;
    [SerializeField] private TextMeshProUGUI taskObjectiveText;
    [SerializeField] private TextMeshProUGUI taskStepText;
    [SerializeField] private TextMeshProUGUI taskStepDescriptionText;

    [SerializeField] private GameObject TaskButtonUIPrefab;
    [SerializeField] private Transform buttonListParent;

    private List<TaskUIContainer> currentTasks;

    private void Awake()
    {
        TaskManager.Instance.OnTaskStart += TaskManager_OnTaskStarted;
        TaskManager.Instance.OnTaskStateChanged += TaskManager_OnTaskStateChanged;
        currentTasks = new List<TaskUIContainer>();
    }

    private void TaskManager_OnTaskStateChanged(Task task)
    {

    }

    private void TaskManager_OnTaskStarted(Task task)
    {
        TaskUIContainer taskUIContainer = new TaskUIContainer();
        taskUIContainer.taskId = task.taskInfo.id;
        taskUIContainer.button = Instantiate(TaskButtonUIPrefab, buttonListParent).GetComponent<Button>();
        currentTasks.Add(taskUIContainer);
        taskUIContainer.button.onClick.AddListener(() => OnButtonClicked(task));
    }

    private void OnButtonClicked(Task task)
    {
        Task.TaskTextInfo taskTextInfo = task.GetTaskTextInfo();
        taskNameText.text = taskTextInfo.taskName;
        taskObjectiveText.text = taskTextInfo.taskObjective;
        taskStepText.text = $"Step {taskTextInfo.stepIndex}/{taskTextInfo.totalSteps}";
        taskStepDescriptionText.text = taskTextInfo.stepDescription;
    }
}
