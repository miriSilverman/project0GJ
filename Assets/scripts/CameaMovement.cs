using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameaMovement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.gameOver && GameController.instance.gameStarted)
        {
            transform.position += speed *Time.deltaTime*Vector3.up; 
        }
        
    }
}
