using System.Collections;
using UnityEngine;

public class CameaMovement : MonoBehaviour
{
    public float speed = 1.8f;
    
    void Update()
    {
        // camera keeps moving up while game is not over
        if (!GameController.instance.gameOver && GameController.instance.gameStarted)
        {
            transform.position += speed *Time.deltaTime*Vector3.up; 
        }
    }
}
