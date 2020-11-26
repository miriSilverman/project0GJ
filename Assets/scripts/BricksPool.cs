using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
  
    
    // public float lowerBoundSizeOfBrick = 4.716518f;
    // public float upperBoundSizeOfBrick = 7.251686f;
    // private float brickHeight = 0.6982102f;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        bricks = new GameObject[numOfBricks];
        
        float xPos = -1f;
        float yPos = -7.89f;
        float brickSize = 16f;
        bricks[0] = (GameObject) Instantiate(brickPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        SpriteRenderer renderer = bricks[0].GetComponent<SpriteRenderer>();
        renderer.size = new Vector2(brickSize, renderer.size.y);
        
        
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
        lastY += Random.Range(2f, 3f);    //random distance between bricks
        bricks[i] = (GameObject) Instantiate(brickPrefab, new Vector2(xPos, lastY), Quaternion.identity);
        SpriteRenderer renderer = bricks[i].GetComponent<SpriteRenderer>();
        
        renderer.size = new Vector2(Random.Range(4f, 7f), renderer.size.y);
        
        renderer.enabled = true;
        float size = renderer.size.x;

        locatePascal(size, xPos);
    }

    private void locatePascal(float size, float xPos)
    {
        if (Random.Range(0, 2) == 1)
        {
            float x = Random.Range(-size / 2f, size / 2f);
            GameObject pascal = (GameObject) Instantiate(pascalPrefab,
                new Vector2(xPos + x, lastY + 0.8f), Quaternion.identity);
        }
    }

    private void relocateLowestBrick()
    {
        float xPos = Random.Range(-7, 7);
        lastY += Random.Range(2f, 3f);    //random distance between bricks
        bricks[lowestBrick].transform.position = new Vector2(xPos, lastY);
        SpriteRenderer renderer = bricks[lowestBrick].GetComponent<SpriteRenderer>();
        
        renderer.size = new Vector2(Random.Range(4f, 7f), renderer.size.y);
        
        renderer.enabled = true;
        bricks[lowestBrick].GetComponent<Collider2D>().enabled = true;
        float size = renderer.size.x;

        locatePascal(size, xPos);
        
        lowestBrick++;
        if (lowestBrick >= numOfBricks)
        {
            lowestBrick = 0;
        }
    }
    
    
}
