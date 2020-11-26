using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksPool : MonoBehaviour
{
    private GameObject[] bricks;
    public GameObject brickPrefab;
    public GameObject pascalPrefab;
    public int numOfBricks = 8;
    public float lastY = -4.89f;
    private int lowestBrick = 0;
    private float timeSinceLastRelocation = 0f;
    public float timeBetweenRelocations = 5f;
    public float lowerBoundSizeOfBrick = 4.716518f;
    public float upperBoundSizeOfBrick = 7.251686f;
    private float brickHeight = 0.6982102f;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        bricks = new GameObject[numOfBricks];
        bricks[0] = (GameObject) Instantiate(brickPrefab, new Vector2(-1f, -7.89f), Quaternion.identity);
        bricks[0].transform.localScale = new Vector3(14.04494f, brickHeight, 1);
        for (int i = 1; i < numOfBricks; i++)
        {
            _locateBrick(i);
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
    

    private void _locateBrick(int i)
    {
        float xPos = Random.Range(-7, 7);
        lastY += 4f;
        bricks[i] = (GameObject) Instantiate(brickPrefab, new Vector2(xPos, lastY), Quaternion.identity);
        float size = Random.Range(lowerBoundSizeOfBrick, upperBoundSizeOfBrick);
        // bricks[i].transform.localScale = new Vector3(size, brickHeight, 1);
        
        bricks[i].GetComponent<Renderer>().enabled = true;
        bricks[i].GetComponent<Collider2D>().enabled = true;

        int b = Random.Range(0, 2);
        if (b == 1)
        {
            float x = Random.Range(-size/2f, size/2f);
            GameObject pascal = (GameObject) Instantiate(pascalPrefab, 
                new Vector2(xPos + x, lastY+0.8f), Quaternion.identity);
        }
        
    }

    private void relocateLowestBrick()
    {
        _locateBrick(lowestBrick);
        
        lowestBrick++;
        if (lowestBrick >= numOfBricks)
        {
            lowestBrick = 0;
        }
    }
    
    
}
