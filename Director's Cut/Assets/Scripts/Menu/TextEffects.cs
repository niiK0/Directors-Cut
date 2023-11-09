using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float defSize;
    [SerializeField] float hoverSize;
    [SerializeField] Color32 defColor;
    [SerializeField] Color32 hoverColor;

    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = hoverSize;
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = defSize;
        text.color = defColor;
    }
}
