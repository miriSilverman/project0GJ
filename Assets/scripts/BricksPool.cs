using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksPool : MonoBehaviour
{
    private GameObject[] bricks;
    public int numOfBricks = 8;
    public GameObject brickPrefab;
    public float lastY = -4.89f;
    private int lowestBrick = 0;
    private bool firstBrickIteration = true;

    private float timeSinceLastRelocation = 0f;

    public float timeBetweenRelocations = 5f;
    // Start is called before the first frame update
    void Start()
    {
        bricks = new GameObject[numOfBricks];
        bricks[0] = (GameObject) Instantiate(brickPrefab, new Vector2(-1f, -7.89f), Quaternion.identity);
        bricks[0].transform.localScale = new Vector3(14.04494f, 0.6982102f, 1);
        for (int i = 1; i < numOfBricks; i++)
        {
            float xPos = Random.Range(-7, 7);
            lastY += 3;
            bricks[i] = (GameObject) Instantiate(brickPrefab, new Vector2(xPos, lastY), Quaternion.identity);

        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameStarted)
        {
            timeSinceLastRelocation += Time.deltaTime;
            if (!GameController.instance.gameOver && timeSinceLastRelocation > timeBetweenRelocations)
            {
                relocateLowestBrick();
                timeSinceLastRelocation = 0f;
            }
        }
    }

    private void relocateLowestBrick()
    {
        if (firstBrickIteration)
        {
            bricks[0].transform.localScale = new Vector3(6.97f, 0.6982102f, 1);
            firstBrickIteration = false;
        }
        float xPos = Random.Range(-7, 7);
        lastY += 3;
        bricks[lowestBrick].transform.position = new Vector3(xPos, lastY, 0);
        bricks[lowestBrick].GetComponent<Renderer>().enabled = true;
        bricks[lowestBrick].GetComponent<Collider2D>().enabled = true;
        
        
        lowestBrick++;
        if (lowestBrick >= numOfBricks)
        {
            lowestBrick = 0;
        }
    }
    
    
}
