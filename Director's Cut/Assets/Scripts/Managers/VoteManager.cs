using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class VoteManager : MonoBehaviourPun
{
    public static VoteManager Instance;

    [SerializeField] private GameObject votingWindow;

    [SerializeField] private VoteCell voteCellPrefab;
    [SerializeField] private Transform voteCellContainer;

    private List<VoteCell> voteCellList = new List<VoteCell>();

    [HideInInspector] private bool hasAlreadyVoted;

    private List<int> playersThatVotedList = new List<int>();
    private List<int> playersThatHaveBeenVotedList = new List<int>();

    private void Awake()
    {
        Instance = this;
    }

    [PunRPC]
    public void StartMeeting() //Deve ser chamado para todos os jogadores
    {
        PopulatePlayerList();
        playersThatVotedList.Clear();
        playersThatHaveBeenVotedList.Clear();
        hasAlreadyVoted = false;
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
            VoteCell newVoteCell = Instantiate(voteCellPrefab, voteCellContainer);
            newVoteCell.Initialize(player.Value, this);
            voteCellList.Add(newVoteCell);
        }
    }

    private void ToggleAllButtons(bool areOn)
    {
        foreach (VoteCell voteCell in voteCellList)
        {
            voteCell.ToggleButton(areOn);
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
        int remainingPlayers = PhotonNetwork.CurrentRoom.PlayerCount; //Ainda não desconta os jogadores mortos

        foreach (VoteCell voteCell in voteCellList)
        {
            if (voteCell.ActorNumber == actorNumber)
            {
                voteCell.UpdateStatus(true);
            }
        }

        //Dá log ao player que votou e ao que foi votado

        if (!playersThatVotedList.Contains(actorNumber))
        {
            playersThatVotedList.Add(actorNumber);
            playersThatHaveBeenVotedList.Add(targetActorNumber);
        }

        if (!PhotonNetwork.IsMasterClient) { return; }
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

        foreach (KeyValuePair<int,int> playerVote in playerVoteCount)
        {
            if (playerVote.Value > mostVotes)
            {
                mostVotes = playerVote.Value;
                mostVotedPlayer = playerVote.Key;
            }
        }

        //Encerra a sessão de votação]
        if (mostVotes >= remainingPlayers / 2) //Aqui também será implementada a funcionalidade "remaining players"
        {
            //Aqui deve-se implementar o código de expulsar o jogador:
            Debug.Log("Foi eliminado o jogador de chave " + mostVotedPlayer + " com " + mostVotes + " votos.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartMeeting();
        }
    }
}
