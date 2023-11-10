using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PianoGame : MonoBehaviour
{
    //Singleton
    public static PianoGame Instance;

    //Variables for the Key
    public Transform spawnPoint;
    public GameObject endPoint;
    public GameObject keyPrefab;
    public float keySpeed = 25.0f;
    public float spawnInterval = 2.0f;
    public bool isKeyPassingThrough;

    private Collider currentPianoKeyCollider;

    private int score;

    private void Awake()
    {
        //Singleton Verification
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Sets the score to zero everytime we start the mini-game
        score = 0;
        
        //Sets the key passing to false so that we cant score
        isKeyPassingThrough = false;

        //Starts the game
        StartCoroutine(SpawnKeys());
    }

    private void Update()
    {
        // Check if a ball has reached the middle
        if (isKeyPassingThrough && Input.GetKeyDown(KeyCode.L))
        {
            //Scores a point
            score++;
            Debug.Log(score);

            //Destroys the key it collided with
            DestroyPianoKey();

            //Resets this variable so we cant score bugged points
            isKeyPassingThrough=false;
            
            //Verifies if the mini-game as been passed
            if(score >= 5)
            {
                Piano.Instance.FinishTask();
            }
        }
    }

    private IEnumerator SpawnKeys()
    {
        //Cycle for spawning keys
        while (true)
        {
            //Instantiates a new key
            GameObject newKey = Instantiate(keyPrefab, spawnPoint.position, Quaternion.identity, transform);
            
            //Sets the speed of the key
            Rigidbody rb = newKey.GetComponent<Rigidbody>();
            rb.velocity = Vector3.left * keySpeed;

            //Waits to call the function again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Called when a ball starts passing through the endpoint trigger
    public void BallStartPassingThrough(Collider pianoKeyCollider)
    {
        //Makes it possible to score points
        isKeyPassingThrough = true;
        
        //Provides the collider to destroy the key when scoring a point
        currentPianoKeyCollider= pianoKeyCollider;
    }

    //Destroys a certain key
    private void DestroyPianoKey()
    {
        Destroy(currentPianoKeyCollider.gameObject);
    }
}
