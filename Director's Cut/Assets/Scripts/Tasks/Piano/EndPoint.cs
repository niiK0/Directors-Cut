using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoKey"))
        {
            // Destroy the piano key
            Destroy(other.gameObject);
        }
    }

    
}
