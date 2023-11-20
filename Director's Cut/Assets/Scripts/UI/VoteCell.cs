using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoteCell : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNameText;
    private bool isSelected = false;

    private int actorNumber;
    public int ActorNumber
    {
        get { return actorNumber; }
    }

    [SerializeField] Button voteButton;
    private VoteManager voteManager;

    private void Awake()
    {
        voteButton.onClick.AddListener(OnVotePressed);
    }

    private void OnVotePressed()
    {
        UpdateStatus(true);
        voteManager.CastVote(actorNumber);
    }

    public void Initialize(PlayerManager player)
    {
        actorNumber = player.photonView.OwnerActorNr;
        playerNameText.text = player.nickname;
        voteManager = VoteManager.Instance;
        voteButton.interactable = player.isAlive;
    }

    public void UpdateStatus(bool selected)
    {
        isSelected = selected;

        if (isSelected)
        {
            playerNameText.color = Color.red;
        }
        else
        {
            playerNameText.color = Color.white;
        }
    }

    public void ToggleButton(bool isInteractible)
    {
        voteButton.interactable = isInteractible;
    }
}