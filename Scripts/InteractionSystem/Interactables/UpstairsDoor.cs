using UnityEngine;

public class UpstairsDoor : InteractableDoor
{
    [SerializeField] private TaskInfoSO targetTask;
    [SerializeField] private int targetTaskStep;

    protected override void ToggleDoor(PlayerInventory playerInventory)
    {
        TaskManager.Instance.TriggerTaskStepCheck(targetTask.id, targetTaskStep);

        if (TaskManager.Instance.GetTaskStepIndex(targetTask.id) == targetTaskStep)
        {
            TextInspectUIManager.Instance.ShowPlayerDialogue("The key should be around here somewhere");
            return;
        }

        base.ToggleDoor(playerInventory);
    }
}
