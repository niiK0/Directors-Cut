using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class VoteManager : MonoBehaviourPun
{
    #region Fields
    //Referências in-code
    public static VoteManager Instance;
    PhotonView view;
    RoleManager roleManager;

    //Referências na interface
    [SerializeField] private GameObject votingWindow;
    [SerializeField] private VoteCell voteCellPrefab;
    [SerializeField] private Transform voteCellContainer;

    //Listas
    private List<VoteCell> voteCellList = new List<VoteCell>();
    private List<int> playersThatVotedList = new List<int>();
    private List<int> playersThatHaveBeenVotedList = new List<int>();

    //Outros fields
    [HideInInspector] private bool hasAlreadyVoted;
    private int skipped;

    //Consts
    public const int skipped_vote_value = 0;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        Instance = this;
        view = GetComponent<PhotonView>();
        roleManager = RoleManager.Instance;
    }
    #endregion

    #region Class Methods
    public void StartMeeting()
    {
        view.RPC("StartMeetingRPC", RpcTarget.All);
    }
    
    [PunRPC]
    private void StartMeetingRPC()
    {
        PopulatePlayerList();
        playersThatVotedList.Clear();
        playersThatHaveBeenVotedList.Clear();
        hasAlreadyVoted = false;
        skipped = 0;
        ToggleAllButtons(true);
        votingWindow.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void PopulatePlayerList()
    {
        //Limpa a lista prévia de jogadores
        for (int i = 0; i < voteCellList.Count; i++)
        {
            Destroy(voteCellList[i].gameObject);
        }

        voteCellList.Clear();

        //Cria uma nova lista de jogadores
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (!roleManager.FindPMByActorNumber(player.Value.ActorNumber).isAlive)
            {
                continue;
            }

            VoteCell newVoteCell = Instantiate(voteCellPrefab, voteCellContainer);
            newVoteCell.Initialize(player.Value, this);
            voteCellList.Add(newVoteCell);
        }
    }

    private void ToggleAllButtons(bool areOn)
    {
        if (roleManager.FindPMByActorNumber(PhotonNetwork.LocalPlayer.ActorNumber).isAlive)
        {
            foreach (VoteCell voteCell in voteCellList)
            {
                voteCell.ToggleButton(areOn);
            }
        }
        else
        {
            foreach (VoteCell voteCell in voteCellList)
            {
                voteCell.ToggleButton(false);
            }
        }

    }

    public void CastVote(int targetActorNumber)
    {
        if (hasAlreadyVoted)
        {
            return;
        }

        hasAlreadyVoted = true;
        ToggleAllButtons(false);
        photonView.RPC("CastPlayerVoteRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, targetActorNumber);
    }

    [PunRPC]
    public void CastPlayerVoteRPC(int actorNumber, int targetActorNumber)
    {
        int remainingPlayers = PhotonNetwork.CurrentRoom.PlayerCount;// - (roleManager.ActorsAlive() + roleManager.DirectorsAlive());

        if (!playersThatVotedList.Contains(actorNumber))
        {
            playersThatVotedList.Add(actorNumber);

            if(targetActorNumber == skipped_vote_value)
            {
                skipped++;
            }
            else
            {
                playersThatHaveBeenVotedList.Add(targetActorNumber);
            }
        }

        if (playersThatVotedList.Count >= remainingPlayers)
        {
            view.RPC("FinishVote", RpcTarget.All);;
        }
    }

    [PunRPC]
    private void FinishVote()
    {
        //Aqui também haverá um check relacionado ao desconto de jogadores mortos

        //Contagem dos votos
        Dictionary<int, int> playerVoteCount = new Dictionary<int, int>();

        foreach (int votedPlayer in playersThatHaveBeenVotedList)
        {
            if (!playerVoteCount.ContainsKey(votedPlayer))
            {
                playerVoteCount.Add(votedPlayer, 0);
            }

            playerVoteCount[votedPlayer]++;
        }

        //Pega o player com mais votos
        int mostVotedPlayer = -1;
        int mostVotes = int.MinValue;

        foreach (KeyValuePair<int, int> playerVote in playerVoteCount)
        {
            if (playerVote.Value > mostVotes)
            {
                mostVotes = playerVote.Value;
                mostVotedPlayer = playerVote.Key;
            }
        }

        if (mostVotes < skipped / 2)
        {
            //Caso a maioria deseje pular o voto:
            Debug.Log("Nenhum jogador foi eliminado");
        }
        else
        {
            //Caso um jogador seja eliminado:
            Debug.Log("Foi eliminado o jogador de chave " + mostVotedPlayer + " com " + mostVotes + " votos.");
            //Implementar aqui o que ocorre quando um jogador é eliminado.
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        votingWindow.SetActive(false);
        roleManager.TryEndGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartMeeting();
        }
    }
    #endregion
}