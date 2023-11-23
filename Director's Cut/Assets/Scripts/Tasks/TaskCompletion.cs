using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TaskCompletion : MonoBehaviourPunCallbacks
{
    public static TaskCompletion Instance;

    [SerializeField] Slider slider;
    [SerializeField] TMP_Text percentageText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        slider.value = 0;
        percentageText.text = "0%";
    }

    public void UpdateCompletion(float value, string text)
    {
        photonView.RPC("UpdateCompletionRPC", RpcTarget.All, value, text);
    }

    [PunRPC]
    public void UpdateCompletionRPC(float value, string text)
    {
        slider.value = value;
        percentageText.text = text;
    }
}
