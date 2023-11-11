using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] List<GameObject> slots = new List<GameObject>();
    [SerializeField] Color activeColor = new Color32(0, 0, 0, 200);
    [SerializeField] Color inactiveColor = new Color32(0, 0, 0, 100);

    private void Awake()
    {
        Instance = this;
    }

    public void StartCooldown(int slot, int time)
    {
        slots[slot].transform.GetChild(2).gameObject.SetActive(true);
        slots[slot].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = time.ToString();
    }

    public void UpdateCooldown(int slot, int time)
    {
        slots[slot].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = time.ToString();
    }

    public void StopCooldown(int slot)
    {
        slots[slot].transform.GetChild(2).gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        int currentSlot = ItemManager.Instance.currentSlot;

        foreach(GameObject slot in slots)
        {
            slot.GetComponent<Image>().color = inactiveColor;
            slot.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (currentSlot != -1)
        {
            slots[currentSlot].GetComponent<Image>().color = activeColor;
        }

        for(int i = 0; i < 3; i++)
        {
            GameObject currentSlotGameObject = ItemManager.Instance.GetItemBySlot(i);

            if (currentSlotGameObject != null)
            {
                slots[i].transform.GetChild(0).gameObject.SetActive(true);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = currentSlotGameObject.GetComponent<Item>().GetItemIcon();
            }
        }
    }
}
