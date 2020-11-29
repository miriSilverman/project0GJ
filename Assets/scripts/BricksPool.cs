using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksPool : MonoBehaviour
{
    private GameObject[] bricks;
    public GameObject brickPrefab;
    public GameObject pascalPrefab;
    public int numOfBricks = 20;
    public float lastY;
    private int lowestBrick = 0;
    private float timeSinceLastRelocation = 0f;
    public float timeBetweenRelocations = 5f;
    private float lastX = -2.82f;
  
    
    
    void Start()
    {
        bricks = new GameObject[numOfBricks];
        
        // first brick is larger
        lastX = -2.214f;
        lastY = -3.481f;
        float brickSize = 10f;
        
        bricks[0] = (GameObject) Instantiate(brickPrefab, new Vector2(lastX, lastY), Quaternion.identity);
        SpriteRenderer renderer = bricks[0].GetComponent<SpriteRenderer>();
        renderer.size = new Vector2(brickSize, renderer.size.y);
        
        for (int i = 1; i < numOfBricks; i++)
        {
            _locateBrick(i);
        }
    }
    

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
    
    // locates the bricks for the first time
    private void _locateBrick(int i)
    {
        float xPos = lastX + Random.Range(-3f, 3f);    // distance between 2 bricks not too far
        while (xPos > 0.17f || xPos < -5.85f)    // checks that its in screen bounds
        {
            xPos = lastX + Random.Range(-3f, 3f);

        }

        lastX = xPos;
        lastY += Random.Range(0.4f, 1.1f);    //random distance between bricks in y axis
        bricks[i] = (GameObject) Instantiate(brickPrefab, new Vector2(xPos, lastY), Quaternion.identity);
        
        SpriteRenderer renderer = bricks[i].GetComponent<SpriteRenderer>();
        renderer.size = new Vector2(Random.Range(2f, 2.4f), renderer.size.y);     // random size of brick
        float size = renderer.size.x;

        locatePascal(size, xPos);    // locates the "diamonds"
    }

    // locates the pascal "diamonds" randomly, but relatively to the brick
    private void locatePascal(float size, float xPos)
    {
        if (Random.Range(0, 2) == 1)    // random chance to have a pascal on brick
        {
            float x = Random.Range(-size / 2f + 0.2f, size / 2f -0.2f);
            GameObject pascal = (GameObject) Instantiate(pascalPrefab,
                new Vector2(xPos + x, lastY + 0.272f), Quaternion.identity);
        }
    }

    // relocates the lowest brick back on top
    private void relocateLowestBrick()
    {
        float xPos = lastX + Random.Range(-3f, 3f);    // distance between 2 bricks not too far
        while (xPos > 0.17f || xPos < -5.85f)    // checks that its in screen bounds
        {
            xPos = lastX + Random.Range(-3f, 3f);

        }

        lastX = xPos;
        lastY += Random.Range(0.4f, 1.1f);    // random distance between bricks in y axis
        bricks[lowestBrick].transform.position = new Vector2(xPos, lastY);
        SpriteRenderer renderer = bricks[lowestBrick].GetComponent<SpriteRenderer>();
        
        renderer.size = new Vector2(Random.Range(2f, 2.4f), renderer.size.y);    // random size of brick
        float size = renderer.size.x;

        locatePascal(size, xPos);
        
        lowestBrick++;
        if (lowestBrick >= numOfBricks)
        {
            lowestBrick = 0;
        }
    }
    
    
}
