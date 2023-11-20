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
    [SerializeField] private VoteCell voteCellPrefab;
    [SerializeField] private Transform voteCellContainer;
    [SerializeField] private TMP_Text voteTimeLeft;

    //Listas
    private List<VoteCell> voteCellList = new List<VoteCell>();
    private List<int> playersThatVotedList = new List<int>();
    private List<int> playersThatHaveBeenVotedList = new List<int>();

    //Outros fields
    [HideInInspector] private bool hasAlreadyVoted;
    private int skipped;

    public float meetingTime;
    private float meetingTimeInternal;

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
        StartCoroutine(TimeMeeting());
    }

    public IEnumerator TimeMeeting()
    {
        meetingTimeInternal = meetingTime;
        while(meetingTimeInternal > 0)
        {
            yield return new WaitForSeconds(1f);
            meetingTimeInternal--;
            voteTimeLeft.text = meetingTimeInternal.ToString();
        }

        if(meetingTimeInternal <= 0 && PhotonNetwork.IsMasterClient)
        {
            view.RPC("FinishVote", RpcTarget.All); ;
        }
    }
    
    private void PopulatePlayerList()
    {
        //Limpa a lista prévia de jogadores
        for (int i = 0; i < voteCellList.Count; i++)
        {
            Destroy(voteCellList[i].gameObject);
        }

        voteCellList.Clear();

        List<PlayerManager> players = RoleManager.Instance.GetPlayerList();

        //Cria uma nova lista de jogadores
        foreach (PlayerManager player in players)
        {

            VoteCell newVoteCell = Instantiate(voteCellPrefab, voteCellContainer);
            newVoteCell.Initialize(player);
            voteCellList.Add(newVoteCell);
        }
    }

    public void CastVote(int targetActorNumber)
    {
        if (hasAlreadyVoted)
        {
            return;
        }

        hasAlreadyVoted = true;
        photonView.RPC("CastPlayerVoteRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, targetActorNumber);
    }

    [PunRPC]
    public void CastPlayerVoteRPC(int actorNumber, int targetActorNumber)
    {
        //int remainingPlayers = PhotonNetwork.CurrentRoom.PlayerCount;// - (roleManager.ActorsAlive() + roleManager.DirectorsAlive());
        int remainingPlayers = PhotonNetwork.CurrentRoom.PlayerCount - RoleManager.Instance.GetDeadPlayers().Count;

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

        if (playersThatVotedList.Count >= remainingPlayers && PhotonNetwork.IsMasterClient)
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
            PlayerManager playerToDie = RoleManager.Instance.FindPMByActorNumber(mostVotedPlayer);
            if (playerToDie.photonView.IsMine)
            {
                RoleManager.Instance.KillPlayer(playerToDie.controller.GetComponent<PlayerController>());
            }
        }

        //DO STUFF TO ENT MEETING
         //NEED TO CLEAN BODIES AND SO ON
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        PlayerController player = RoleManager.Instance.GetMyPlayerManager().controller.GetComponent<PlayerController>();
        player.MovePlayer(spawnPoint);
        player.freezePlayer = false;
        player.GetComponentInChildren<Animator>().SetBool("isSitting", false);

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