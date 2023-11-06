using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = new Color32(59, 24, 13,255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = new Color32(130, 82, 67,255);
    }
}
