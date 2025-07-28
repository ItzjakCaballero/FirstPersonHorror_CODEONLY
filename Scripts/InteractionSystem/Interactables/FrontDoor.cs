using UnityEngine;

public class FrontDoor : InteractableDoor
{
    [SerializeField] private TaskInfoSO targetTask;
    [SerializeField] private int targetTaskStep;

    protected override void ToggleDoor(PlayerInventory playerInventory)
    {
        TaskManager.Instance.TriggerTaskStepCheck(targetTask.id, targetTaskStep);

        if(TaskManager.Instance.GetTaskStepIndex(targetTask.id) == targetTaskStep)
        {
            TextInspectUIManager.Instance.ShowPlayerDialogue("I should get the flashlight and key prepared before going in");
            return;
        }

        base.ToggleDoor(playerInventory);
    }
}
