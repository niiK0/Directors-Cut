using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int cooldownDuration = 10;

    public bool isEquipped = false;
    public bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }


    // Called in Update() , just checking for inputs and handling them
    public void GetInput()
    {
        // Equip / Unequip
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip();
        }

        // Kill / Attack
        if (Input.GetMouseButtonDown(0))
        {
            Kill();
        }
    }

    // Equip / Unequip method
    private void Equip()
    {
        if (!isEquipped)
        {
            // Equip
            isEquipped = true;
        }
        else if (isEquipped)
        {
            // Unequip
            isEquipped = false;
        }
    }

    // Kill method
    private void Kill()
    {
        // Kill player

        // NOT EQUIPPED
        if (!isEquipped)
        {
            return;
        }

        // EQUIPPED BUT ON COOLDOWN
        if (isEquipped && !isActive)
        {
            return;
        }

        // KILL HERE
        if(isEquipped && isActive)
        {
            // Kill logic HERE


            StartCoroutine(StartCooldown());
        }
    }


    // Manages the weapons cooldown
    public IEnumerator StartCooldown()
    {
        isActive = false;

        yield return new WaitForSeconds(cooldownDuration);

        isActive = true;
    }
}
