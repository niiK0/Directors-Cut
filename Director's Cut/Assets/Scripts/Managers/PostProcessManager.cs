using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager Instance;

    [SerializeField] VolumeProfile[] volumes;
    [SerializeField] Volume GlobalVolume;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalVolume.profile = volumes[0];   
    }

    public void SetVolume(int index)
    {
        GlobalVolume.profile = volumes[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
