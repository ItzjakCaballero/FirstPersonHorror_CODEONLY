using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUpdateNotificationUI : MonoBehaviour
{
    private struct TextInfo
    {
        public string taskName;
        public string info;
    }

    [SerializeField] private GameObject parent;
    [SerializeField] private TextMeshProUGUI taskNameText;
    [SerializeField] private TextMeshProUGUI infoText;

    private Queue<TextInfo> textInfoQueue;
    private float updateTimer = 0f;
    private float updateTimerMax = .3f;
    private bool DisplayingInfo = false;

    private void Start()
    {
        Hide();
        textInfoQueue = new Queue<TextInfo>();
        TaskManager.Instance.OnTaskStateChanged += TaskManager_OnTaskStateChanged;
        TaskManager.Instance.OnTaskAdvanced += TaskManager_OnTaskAdvanced;
    }

    private void TaskManager_OnTaskAdvanced(Task task)
    {
        TextInfo displayInfo = new TextInfo();
        displayInfo.taskName = task.taskInfo.name;
        displayInfo.info = $"Step: {task.GetCurrentStepIndex() + 1}/{task.taskInfo.taskStepPrefabs.Length}";
        textInfoQueue.Enqueue(displayInfo);
    }

    private void TaskManager_OnTaskStateChanged(Task task)
    {
        TextInfo displayInfo = new TextInfo();
        displayInfo.taskName = task.taskInfo.name;
        switch (task.state)
        {
            case TaskState.Started:
                displayInfo.info = "Started";
                break;
            case TaskState.Finished:
                displayInfo.info = "Finished";
                break;
        }

        textInfoQueue.Enqueue(displayInfo);
    }

    private void Update()
    {
        if (textInfoQueue.Count > 0 && !DisplayingInfo)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= updateTimerMax)
            {
                updateTimer = 0f;
                StartCoroutine(DisplayInfo());
            }
        }
    }

    private IEnumerator DisplayInfo()
    {
        TextInfo textInfo = textInfoQueue.Dequeue();
        taskNameText.text = textInfo.taskName;
        infoText.text = textInfo.info;
        Show();

        float timeToWait = 2f;
        yield return new WaitForSeconds(timeToWait);

        Hide();
    }

    private void Show()
    {
        parent.SetActive(true);
        DisplayingInfo = true;
    }

    private void Hide()
    {
        parent.SetActive(false);
        DisplayingInfo = false;
    }
}
