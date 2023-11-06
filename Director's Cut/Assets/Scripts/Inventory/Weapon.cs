using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField][Range(10.0f, 60.0f)] private int cooldownDuration = 10;
    [SerializeField][Range(1.0f, 15.0f)] private float maxRange = 3.0f;

    private float attackRate = 1.0f;

    private GameObject playerInRange;

    private bool isEquipped = false;
    private bool isActive = false;
    private float nextAttackTime = 0.0f;

    private void Start()
    {
        if (isActive == false) StartCooldown();
    }

    void Update()
    {
        GetInput();
    }


    public void GetInput()
    {
        // Equip / Unequip
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LittleDebug();
            Equip();
            
        }

        // Kill / Attack
        if (Input.GetMouseButtonDown(0))
        {
            Kill();
            LittleDebug();
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
            WeaponManager weaponManager = WeaponManager.Instance;

            // ATTACK ANIMATION

            // Kill logic HERE
            if(nextAttackTime <= 0.0f) 
            {
                if (IsEnemyInRange())
                {
                    weaponManager.KillPlayer(playerInRange.GetComponent<PlayerManager>());
                    StartCoroutine(StartCooldown());
                }
            
                nextAttackTime = Time.time + 1.0f/attackRate;
            }
        }
    }


    public bool isOnCooldown()
    {
        return isActive;
    }


    bool IsEnemyInRange()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRange))
        {
            // Check if the object hit by the raycast is a player
            if (hit.collider.CompareTag("Player"))
            {
                playerInRange = hit.collider.gameObject;
                return true;
            }
        }

        return false;
    }
    
    public void LittleDebug()
    {
        Console.WriteLine("Is Equipped = " + isEquipped + 
                          " Is Active = " + isActive +
                          " cooldownDuration = " + cooldownDuration +
                          " nextAttackTime = " + nextAttackTime);
    }


    // Manages the weapons cooldown
    public IEnumerator StartCooldown()
    {
        isActive = false;

        yield return new WaitForSeconds(cooldownDuration);

        isActive = true;
    }
}
