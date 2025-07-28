using UnityEngine;

[RequireComponent(typeof(TaskStarter))]
public class InteractableTaskStarter : Interactable
{
    TaskStarter taskStarter;

    private void Start()
    {
        taskStarter = GetComponent<TaskStarter>();
    }

    protected override void BaseInteract(GameObject player)
    {
        taskStarter.StartTask();
    }
}
