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
    }
    #endregion

    #region Class Methods
    [PunRPC]
    public void StartMeeting()
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

        if (playersThatVotedList.Count == remainingPlayers)
        {
            view.RPC("FinishVote", RpcTarget.All);
        }
    }

    [PunRPC]
    private void FinishVote()
    {
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

        votingWindow.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            view.RPC("StartMeeting", RpcTarget.All);
        }
    }
    #endregion
}

//A fazer:
//-> Funcionalidade skip vote (Feito)
//-> Fechar a janela do voto uma vez que esteja completo (Feito)
//-> Não contar jogadores mortos
//-> Indicadores visuais de quem foi eliminado