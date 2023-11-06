using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    
    private RoleManager roleManager = RoleManager.Instance;

    private List<PlayerManager> playersAlive;

    [SerializeField] private GameObject weapon;

    private int playerCount = 0;

    private void Start()
    {
        playersAlive = roleManager.GetPlayerList();
        playerCount = playersAlive.Count;

        foreach (PlayerManager player in playersAlive)
        {
            if(player.isDirector == true)
            {
                GameObject weaponToGive = Instantiate(weapon);
                player.GetComponent<ItemManager>().AddItem(weaponToGive);
            }
        }
    }

    public void KillPlayer(PlayerManager playerToDie)
    {
        playerToDie.isAlive = false;

        playersAlive.Remove(playerToDie);

        playerToDie.GetComponent<Ghost>().SetGhostMode(playerToDie!.isAlive);

        playerCount--;
    }

    public List<PlayerManager> GetPlayersAlive()
    {
        return playersAlive;
    }

    public bool IsPlayerAlive(PlayerManager player)
    {
        if(playersAlive.Contains(player)) return true;
        return false;
    }
}
