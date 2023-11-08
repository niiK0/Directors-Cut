using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PianoGame : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject ballPrefab;
    public float ballSpeed = 5.0f;
    public float spawnInterval = 2.0f;
    public TextMeshProUGUI scoreText;
    public Button clickButton;

    private int score;
    private bool canScore;

    private void Start()
    {
        score = 0;
        canScore = false;

        //Corrotina que spawna as teclas
        StartCoroutine(SpawnBalls());
    }

    private void Update()
    {
        // Check if a ball has reached the middle
        if (canScore && Input.GetKeyDown(KeyCode.L))
        {
            score++;
            scoreText.text = "Score: " + score;
            canScore = false;
        }
    }

    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            rb.velocity = Vector3.left * ballSpeed;
            Destroy(newBall, 5.0f); // Destroy the ball after a few seconds

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
