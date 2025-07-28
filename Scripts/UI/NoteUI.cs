using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    private void OnDisable()
    {
        messageText.text = string.Empty;
    }

    public void SetMessageText(string text)
    {
        messageText.text = text;
    }
}
