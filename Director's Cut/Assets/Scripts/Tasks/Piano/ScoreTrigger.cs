using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoKey"))
        {
            // Assuming PianoGame is the parent of this script's GameObject
            PianoGame.Instance.BallStartPassingThrough(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PianoKey"))
        {
            //Assuming PianoGame is the parent of this script's GameObject
            PianoGame.Instance.isKeyPassingThrough = false;
        }
    }
}
