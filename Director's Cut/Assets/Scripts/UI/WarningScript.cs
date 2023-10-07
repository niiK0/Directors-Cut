using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningScript : MonoBehaviour
{
    private Image warning;

    [SerializeField] private float warningDuration;

    private void Start()
    {
        warning = GetComponent<Image>();
        warning.enabled = false;
    }

    public IEnumerator CallWarning()
    {
        warning.enabled = true;
        yield return new WaitForSeconds(warningDuration);
        warning.enabled = false;      
    }
}
