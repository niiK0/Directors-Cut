using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseXEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Vector3 defSize;
    [SerializeField] Vector3 hoverSize;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defSize;
    }
}
