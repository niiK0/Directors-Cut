using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessageScaler : MonoBehaviour
{
    [SerializeField] TMP_Text chatMessage;
    [SerializeField] RectTransform rectTransform;

    // Start is called before the first frame update
    void Awake()
    {
        chatMessage = gameObject.GetComponent<TMP_Text>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void SetText(string sender, string message)
    {
        chatMessage.text = $"<color=green>{sender}</color><color=white>: {message}</color>";
        int height = chatMessage.textInfo.lineCount;
        rectTransform.sizeDelta = new Vector2(600, 40 + 20 * height);
    }
}
