using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public AudioSource audioSource;
    private AudioClip gameOverSound;

    public Text scoreText;
    public GameObject gameOverText;
    public GameObject winningText;
    public bool gameStarted = false;
    public bool gameOver = false;
    private int score = 0;
    public int winningScore = 10;

    


    private void Awake()
    {
        // only one game controller
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    

    void Update()
    {
        if ((gameOver && Input.GetKey(KeyCode.Space)) || Input.GetKey(KeyCode.R))
        {
            // reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
    
    // plays the gameOver sound
    IEnumerator playGameOverSound()
    {
        audioSource.clip = gameOverSound;
        audioSource.Play();
        yield return new WaitUntil(() => !audioSource.isPlaying);
    }

    private void Start()
    {
        gameOverSound = (AudioClip) Resources.Load("GameOver");
    }

    // updates the score
    public void scoring()
    {
        if (gameOver)
        {
            return;
        }
        
        score++;
        scoreText.text = "Score: " + score.ToString();
        if (score >= winningScore)
        {
            winning();
        }
    }

    // shows winning text on screen
    public void winning()
    {
        gameOver = true;
        winningText.SetActive(true);
    }

    // plays gameOver sound and shows gameOver text
    public void playerDied()
    {
        StartCoroutine(playGameOverSound());
        gameOver = true;
        gameOverText.SetActive(true);
    }
}
