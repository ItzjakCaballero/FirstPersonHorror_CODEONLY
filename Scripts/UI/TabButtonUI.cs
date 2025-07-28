using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButtonUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TabGroup tabGroup;

    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void SetTabColor(Color color)
    {
        background.color = color;
    }
}
