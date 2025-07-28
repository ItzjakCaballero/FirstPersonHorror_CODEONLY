using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private Color idleColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private List<GameObject> objectsToSwap = new List<GameObject>();
    [SerializeField] private TabButtonUI defaultTabShown;

    private List<TabButtonUI> tabButtonsList;

    private void OnEnable()
    {
        if (tabButtonsList != null)
        {
            OnTabSelected(defaultTabShown);
        }
    }

    public void Subscribe(TabButtonUI tabButton)
    {
        if (tabButtonsList == null)
        {
            tabButtonsList = new List<TabButtonUI>();
        }

        tabButtonsList.Add(tabButton);
    }

    public void OnTabSelected(TabButtonUI tabButton)
    {
        ResetTabs();
        tabButton.SetTabColor(selectedColor);

        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    private void ResetTabs()
    {
        foreach (TabButtonUI tabButton in tabButtonsList)
        {
            tabButton.SetTabColor(idleColor);
        }
    }
}
