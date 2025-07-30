using System.Collections.Generic;
using UnityEngine;

public class InteractableLightSwitch : Interactable
{
    [SerializeField] private List<Light> lightsToToggle;

    private InteractableItemDetails textInspectItem;
    private bool isLightOn = false;

    protected void Awake()
    {
        textInspectItem = GetComponent<InteractableItemDetails>();
        textInspectItem.SetObjectName("Turn on");
    }

    protected override void BaseInteract(GameObject player)
    {
        if (isLightOn)
        {
            foreach (Light light in lightsToToggle)
            {
                light.enabled = false;
            }
            textInspectItem.SetObjectName("Turn on");
        }
        else
        {
            foreach(Light light in lightsToToggle)
            {
                light.enabled = true;
            }
            textInspectItem.SetObjectName("Turn off");
        }
        isLightOn = !isLightOn;
    }
}
