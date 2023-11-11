using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField][Range(10.0f, 60.0f)] private int cooldownDuration = 10;
    [SerializeField][Range(1.0f, 15.0f)] private float maxRange = 3.0f;

    [SerializeField] float attackRate = 5f;

    private float nextAttackTime = 0f;

    private bool canAttack = false;

    private GameObject playerInRange;

    private Item item;

    private void Start()
    {
        item = GetComponent<Item>();
        AttackCooldown();
    }

    void Update()
    {
        GetInput();

        if (!canAttack)
        {
            if(nextAttackTime <= 0f)
            {
                InventoryManager.Instance.StopCooldown(item.slotNumber);
                canAttack = true;
            }

            if(nextAttackTime > 0f && nextAttackTime <= attackRate)
            {
                nextAttackTime -= 1f * Time.deltaTime;
                InventoryManager.Instance.UpdateCooldown(item.slotNumber, Mathf.RoundToInt(nextAttackTime));
            }
        }
    }


    public void GetInput()
    {
        // Kill / Attack
        if (Input.GetMouseButtonDown(0))
        {
            Kill();
            LittleDebug();
        }
    }

    private void Kill()
    {
        // NOT EQUIPPED/IN COOLDOWN
        if (!item.isEquipped || !canAttack)
            return;

        // Kill logic HERE
        if (IsEnemyInRange())
        {
            RoleManager.Instance.KillPlayer(playerInRange.GetComponent<PlayerController>());
            AttackCooldown();
        }
    }


    public bool isOnCooldown()
    {
        return canAttack;
    }

    private void AttackCooldown()
    {
        canAttack = false;
        nextAttackTime = attackRate;
        InventoryManager.Instance.StartCooldown(item.slotNumber, Mathf.RoundToInt(nextAttackTime));
    }

    bool IsEnemyInRange()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
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
        Console.WriteLine("Is Equipped = " + item.isEquipped + 
                          " can attack = " + canAttack +
                          " cooldownDuration = " + cooldownDuration +
                          " nextAttackTime = " + nextAttackTime);
    }
}
