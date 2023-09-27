using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RefreshEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    private bool hovering = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        MenuManager.Instance.GetComponent<Launcher>().RefreshRoomList();
        Debug.Log("refreshed rooms");
    }

    private void Update()
    {
        if (hovering)
        {
            if (transform.rotation.z <= -360)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            transform.Rotate(new Vector3(0f, 0f, -1f));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
