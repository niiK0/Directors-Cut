using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance;

    [SerializeField] Recorder recorder;

    private void Awake()
    {
        Instance = this;
    }

    public void MutePlayer(bool mute)
    {
        recorder.TransmitEnabled = !mute;
    }
}
