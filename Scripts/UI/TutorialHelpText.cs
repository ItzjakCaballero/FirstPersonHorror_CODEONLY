using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialHelpText : MonoBehaviour
{
    public static TutorialHelpText Instance { get; private set; }

    [SerializeField] private GameObject parent;
    [SerializeField] private TextMeshProUGUI textArea;

    private Queue<string> textDisplayQueue;
    private float updateTimer = 0f;
    private float updateTimerMax = .3f;
    private bool DisplayingInfo = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple TutorialHelpText's active");
        }

            textDisplayQueue = new Queue<string>();
        Hide();
    }

    private void Update()
    {
        if (textDisplayQueue.Count > 0 && !DisplayingInfo)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= updateTimerMax)
            {
                updateTimer = 0f;
                StartCoroutine(DisplayText());
            }
        }
    }

    private IEnumerator DisplayText()
    {
        string text = textDisplayQueue.Dequeue();
        textArea.text = text;
        Show();

        float timeToWait = 2f;
        yield return new WaitForSeconds(timeToWait);

        Hide();
    }

    public void DisplayText(string text)
    {
        textDisplayQueue.Enqueue(text);
    }

    public void Show()
    {
        parent.SetActive(true);
        DisplayingInfo = true;
    }

    public void Hide()
    {
        DisplayingInfo = false;
        parent.SetActive(false);
    }
}
