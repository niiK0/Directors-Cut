using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightFlickerScript : MonoBehaviour
{
    [SerializeField][Range(1.0f, 15.0f)] float minTime = 5.0f;
    [SerializeField][Range(15.0f, 40.0f)] float maxTime = 15.0f;
    [SerializeField][Range(0.1f, 2.0f)] float waitTime = 0.5f;


    private bool isFlickering = false;
    private float delayTime;

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(FlickerCoroutine());
        }
    }

    IEnumerator FlickerCoroutine()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = true;
        delayTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(delayTime);
        this.gameObject.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        isFlickering = false;
    }
}
