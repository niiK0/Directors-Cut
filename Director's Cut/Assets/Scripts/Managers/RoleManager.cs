using UnityEngine;
using Photon.Pun;
using System.Linq;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class RoleManager : MonoBehaviourPunCallbacks
{
    //Padrão singleton
    public static RoleManager Instance;

    //Lista que é atualizada com os PlayerManagers dos jogadores
    [SerializeField] List<PlayerManager> players;

    [SerializeField] TMP_Text playerTypeText;

    private bool loaded = false;
    private int nOfDirectors = 2;

    private const string DirectorsPropertyKey = "Directors";

    public List<PlayerManager> GetPlayerList()
    {
        return players;
    }

    private void Update()
    {
        if (!loaded)
        {
            if(players.Count() == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                loaded = true;
                if(PhotonNetwork.IsMasterClient)
                    SetUpRoles();

                return;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayerTypeUI(bool isDirector)
    {
        playerTypeText.text = isDirector ? "Director" : "Actor";
    }

    public void AddPlayer(PlayerManager player)
    {
        players.Add(player);
    }

    //Atribui aos PlayerManagers na lista de players a role de Diretor, se tiverem sido sorteados
    private void AssignRoles()
    {
        int nOfPlayers = players.Count;

        HashSet<int> aux = new HashSet<int>();
        bool canProceed = true;

        //Sorteia <nº de Diretores> números aleatórios de 0 a <nº de Jogadores>. Esses serão os diretores
        for (int i = 0; i < nOfDirectors; i++)
        {
            int n;
            do
            {
                n = Random.Range(0, nOfPlayers);
                Debug.Log("Generated " + n + " to try as director");
                if(aux.Contains(n))
                    canProceed = false;

                Debug.Log("Trying " + aux.Contains(n) + " for contains in aux var, can proceed = " + canProceed);
            } while (!canProceed);

            aux.Add(players[n].cachedActorNumber);
            Debug.Log("Chose this guy for director : " + players[n].cachedActorNumber);
        }

        Hashtable directorsRoomProperties = new Hashtable();
        directorsRoomProperties[DirectorsPropertyKey] = aux.ToArray();
        PhotonNetwork.CurrentRoom.SetCustomProperties(directorsRoomProperties);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(DirectorsPropertyKey))
        {
            // Update the list of director actor numbers when the room property changes
            int[] directorUpdatedList = (int[])propertiesThatChanged[DirectorsPropertyKey];

            foreach(PlayerManager player in players)
            {
                if (directorUpdatedList.Contains(player.cachedActorNumber)){
                    Debug.Log("Changing player with actor number " + player.cachedActorNumber + " to director");
                    player.isDirector = true;
                }
            }

            foreach(PlayerManager player in players)
            {
                Debug.Log("brotha " + player.cachedActorNumber + " director state = " + player.isDirector);
                player.SyncPlayerTypeUI();
            }
        }
    }

    //Função a ser chamada ao iniciar do jogo. Define as roles. Ainda não é chamada por ninguém, mas já está pronta para utilização.
    private void SetUpRoles()
    {
        AssignRoles();
        Debug.Log("Roles setup finished!");
    }
}
