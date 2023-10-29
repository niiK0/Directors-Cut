using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoteCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    private bool isSelected = false;

    private int actorNumber;
    public int ActorNumber
    {
        get { return actorNumber; }
    }

    [SerializeField] private Button voteButton;
    private VoteManager voteManager;

    private void Awake()
    {
        voteButton.onClick.AddListener(OnVotePressed);
    }

    private void OnVotePressed()
    {
        voteManager.CastVote(actorNumber);
    }

    public void Initialize(Player player, VoteManager voteManager)
    {
        actorNumber = player.ActorNumber;
        playerNameText.text = player.NickName;
        this.voteManager = voteManager;
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
