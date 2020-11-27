using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameaMovement : MonoBehaviour
{
    public float speed = 1.8f;
    
    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.gameOver && GameController.instance.gameStarted)
        {
            transform.position += speed *Time.deltaTime*Vector3.up; 
        }
    }
}
