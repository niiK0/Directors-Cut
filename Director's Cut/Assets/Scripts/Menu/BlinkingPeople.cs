using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingPeople : MonoBehaviour
{
    [SerializeField] GameObject[] bodies;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float blinkTime;

    private GameObject currentBody;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(waitTime);

            int randomBody = Random.Range(0, bodies.Length);

            currentBody = bodies[randomBody];

            currentBody.SetActive(false);

            yield return new WaitForSeconds(blinkTime);

            currentBody.SetActive(true);
        }
    }
}
