using UnityEngine;

public class ClockPuzzle : Puzzle
{
    private const int TIME_HOUR_SOLUTION = 11;
    private const int TIME_MINUTE_SOLUTION = 42;

    [SerializeField] private Transform minuteHandVisual;
    [SerializeField] private Transform hourHandVisual;

    private int timeHour;
    private int timeMinute;

    private void Start()
    {
        minuteHandVisual.rotation = Quaternion.identity;
        hourHandVisual.rotation = Quaternion.identity;
        timeHour = 12;
        timeMinute = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseTime();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            DecreaseTime();
        }
    }

    public void IncreaseTime()
    {
        int newTimeMin = 0;
        int newTimeHr = 0;
        if (timeMinute >= 59)
        {
            if (timeHour >= 12)
            {
                newTimeHr = 1;
            }
            else
            {
                newTimeHr = timeHour + 1;
            }
            newTimeMin = 0;
        }
        else
        {
            newTimeHr = timeHour;
            newTimeMin = timeMinute + 1;
        }
        ChangeTime(newTimeHr, newTimeMin);
    }

    public void DecreaseTime()
    {
        int newTimeMin = 0;
        int newTimeHr = 0;
        if (timeMinute <= 0)
        {
            if (timeHour <= 1)
            {
                newTimeHr = 12;
            }
            else
            {
                newTimeHr = timeHour - 1;
            }
            newTimeMin = 59;
        }
        else
        {
            newTimeHr = timeHour;
            newTimeMin = timeMinute - 1;
        }
        ChangeTime(newTimeHr, newTimeMin);
    }

    private void ChangeTime(int newTimeHour, int newTimeMinute)
    {
        timeHour = newTimeHour;
        timeMinute = newTimeMinute;
        CheckForSolution();
        UpdateVisuals();
        Debug.Log($"{timeHour}:{timeMinute}");
    }

    private void UpdateVisuals()
    {
        float hourHandDegrees = (timeHour + timeMinute / 60f) * (360 / 12);
        float minuteHandDegrees = timeMinute * (360 / 60);

        hourHandVisual.localRotation = Quaternion.Euler(0, 0, hourHandDegrees);
        minuteHandVisual.localRotation = Quaternion.Euler(0, 0, minuteHandDegrees);
    }

    protected override void CheckForSolution()
    {
        if (timeHour == TIME_HOUR_SOLUTION && timeMinute == TIME_MINUTE_SOLUTION)
        {
            MarkAsSolved();
        }
    }
}
