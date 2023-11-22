using UnityEngine;
using Photon.Pun;
using System.Linq;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine.UIElements;
using System.IO;

public class RoleManager : MonoBehaviourPunCallbacks
{
    #region Fields
    //Padrão singleton
    public static RoleManager Instance;

    //Lista que é atualizada com os PlayerManagers dos jogadores
    [SerializeField] List<PlayerManager> players;

    [SerializeField] TMP_Text playerTypeText;

    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI winnerTeamText;
    [SerializeField] private Transform winnersContainer;

    [SerializeField] private GameObject weaponPrefab;

    private bool loaded = false;
    private int nOfDirectors = 1;

    private const string directors_property_key = "Directors";

    private const string directors_team_name = "DIRECTORS";
    private const string actors_team_name = "ACTORS";
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        Instance = this;
        players = new List<PlayerManager>();
    }

    private void Update()
    {
        if (!loaded)
        {
            if (players.Count() == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                loaded = true;
                if (PhotonNetwork.IsMasterClient)
                    AssignRoles();

                return;
            }
        }
    }
    #endregion

    #region Class Methods
    public List<PlayerManager> GetPlayerList()
    {
        return players;
    }

    public void KillPlayer(PlayerController playerToDie)
    {
        //set ghost of playerToDie
        //playerToDie.GetComponent<Ghost>().SetGhostModeRPC();
        playerToDie.playerManager.photonView.RPC("KillPlayer", RpcTarget.All, playerToDie.photonView.ViewID, playerToDie.transform.position, playerToDie.transform.rotation);
    }

    public PlayerManager GetMyPlayerManager()
    {
        foreach (PlayerManager player in players)
        {
            if (player.photonView.AmOwner)
            {
                return player;
            }
        }

        return null;
    }

    public void AddPlayer(PlayerManager player)
    {
        players.Add(player);
    }

    public List<PlayerManager> GetDeadPlayers()
    {
        List<PlayerManager> deadPlayers = new List<PlayerManager>();

        foreach(PlayerManager player in players)
        {
            if (!player.isAlive)
            {
                deadPlayers.Add(player);
            }
        }

        return deadPlayers;
    }

    public PlayerManager FindPMByActorNumber(int actorNumber)
    {
        foreach (PlayerManager playerManager in players)
        {
            if (playerManager.cachedActorNumber == actorNumber)
            {
                return playerManager;
            }
        }

        return null;
    }

    #region Roles Methods

    public void SetPlayerTypeUI(bool isDirector)
    {
        playerTypeText.text = isDirector ? "Director" : "Actor";
    }

    //Atribui aos PlayerManagers na lista de players a role de Diretor, se tiverem sido sorteados
    private void AssignRoles()
    {
        int nOfPlayers = players.Count;

        HashSet<int> aux = new HashSet<int>(); //HashSet que contém os DIRETORES
        bool canProceed = true;

        //Sorteia <nº de Diretores> números aleatórios de 0 a <nº de Jogadores>. Esses serão os diretores
        for (int i = 0; i < nOfDirectors; i++)
        {
            int n;
            do
            {
                n = Random.Range(0, nOfPlayers);
                Debug.Log("Generated " + n + " to try as director");
                if (aux.Contains(n))
                {
                    canProceed = false;
                }

                Debug.Log("Trying " + aux.Contains(n) + " for contains in aux var, can proceed = " + canProceed);
            } while (!canProceed);

            aux.Add(players[n].cachedActorNumber);
            Debug.Log("Chose this guy for director : " + players[n].cachedActorNumber);
        }

        Hashtable directorsRoomProperties = new Hashtable();
        directorsRoomProperties[directors_property_key] = aux.ToArray();
        PhotonNetwork.CurrentRoom.SetCustomProperties(directorsRoomProperties);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(directors_property_key))
        {
            // Update the list of director actor numbers when the room property changes
            int[] directorUpdatedList = (int[])propertiesThatChanged[directors_property_key];

            foreach(PlayerManager player in players)
            {
                if (directorUpdatedList.Contains(player.cachedActorNumber)){
                    Debug.Log("Changing player with actor number " + player.cachedActorNumber + " to director");
                    player.isDirector = true;
                    GameObject weapon = Instantiate(weaponPrefab);
                    weapon.GetComponent<Item>().itemIdentifier = "weapon" + player.cachedActorNumber;

                    if (player.photonView.IsMine)
                    {
                        weapon.GetComponent<Item>().Interact(player.controller);
                        weapon.GetComponent<Item>().Unequip();
                    }

                    if (!player.photonView.IsMine)
                    {
                        weapon.GetComponent<Item>().SetPlayer(player.controller);
                        weapon.GetComponent<Weapon>().enabled = false;
                        weapon.GetComponent<Item>().UnequipVisual();
                    }

                }
            }

            foreach(PlayerManager player in players)
            {
                Debug.Log("brotha " + player.cachedActorNumber + " director state = " + player.isDirector);
                player.SyncPlayerTypeUI();
            }
        }
    }
    #endregion

    #region GameEnd Methods
    //Deve ser chamado sempre que um jogador morrer.
    public void TryEndGame()
    {
        int directorsRemaining = DirectorsAlive();
        int actorsRemaining = ActorsAlive();

        if (directorsRemaining >= actorsRemaining)
        {
            //Vitória dos diretores
            Debug.Log("Vencem os diretores!");
            EndScreen(false);
        }
        else if (directorsRemaining == 0) //Adicionar também a vitória por tasks!
        {
            //Vitória dos atores
            Debug.Log("Vencem os atores!");
            EndScreen(true);
        }
    }

    public int DirectorsAlive()
    {
        int nOfDirectorsAlive = 0;
        for (int i = 0; i < players.Count(); i++)
        {
            if (players[i].isDirector && players[i].isAlive)
            {
                nOfDirectorsAlive++;
            }
        }
        return nOfDirectorsAlive;
    }

    public int ActorsAlive()
    {
        int nOfActorsAlive = 0;
        for (int i = 0; i < players.Count(); i++)
        {
            if (!players[i].isDirector && players[i].isAlive)
            {
                nOfActorsAlive++;
            }
        }
        return nOfActorsAlive;
    }

    private void EndScreen(bool actorsWon)
    {
        endScreen.SetActive(true);
        if (actorsWon)
        {
            winnerTeamText.text = actors_team_name;
            winnerTeamText.color = Color.green;

            foreach (PlayerManager player in players)
            {
                if (!player.isDirector)
                {
                    TextMeshProUGUI actorName = new TextMeshProUGUI();
                    actorName.text = player.GetComponent<PhotonView>().Owner.NickName;
                    actorName.color = Color.green;
                    Object.Instantiate(actorName, winnersContainer);
                }
            }
        }
        else
        {
            winnerTeamText.text = directors_team_name;
            winnerTeamText.color = Color.red;

            foreach (PlayerManager player in players)
            {
                if (player.isDirector)
                {
                    TextMeshProUGUI directorName = new TextMeshProUGUI();
                    directorName.text = player.GetComponent<PhotonView>().Owner.NickName;
                    directorName.color = Color.red;
                    Instantiate(directorName, winnersContainer);
                }
            }
        }
    }
    #endregion

    #endregion
}
