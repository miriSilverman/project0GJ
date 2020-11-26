using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public Text scoreText;
    public GameObject gameOverText;
    public GameObject winningText;
    public bool gameOver = false;
    public bool gameStarted = false;
    private int score = 0;
    public int winningScore = 5;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if ((gameOver && Input.GetKey(KeyCode.Space)) || Input.GetKey(KeyCode.R))
        {
            // reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

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

    public void winning()
    {
        gameOver = true;
        winningText.SetActive(true);
    }

    public void playerDied()
    {
        gameOver = true;
        gameOverText.SetActive(true);
    }
}
