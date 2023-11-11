using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    private List<PlayerManager> playersAlive;

    [SerializeField] private GameObject weapon;
     
    [SerializeField] private GameObject deadBodyPlayerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayersAliveList(List<PlayerManager> playersAlive)
    {
        this.playersAlive = playersAlive;
    }

    public void KillPlayer(PlayerManager playerToDie)
    {
        playerToDie.isAlive = false;

        Instantiate(deadBodyPlayerPrefab, playerToDie.transform.position, playerToDie.transform.rotation);
        
        playersAlive.Remove(playerToDie);
        
        //playerToDie.GetComponent<Ghost>().SetGhostMode(true);
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
