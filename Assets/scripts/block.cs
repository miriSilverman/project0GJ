using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public GameObject go;
    public float speed;
    private float lowerScreenBound;
    private float upperScreenBound;
    public int upperScaleRange = 7;
    public int lowerScaleRange = 3;

    public Renderer renderer;

    public BoxCollider2D collider;
    // public bool gameStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        // go.GetComponent<Rigidbody2D>().velocity = Vector2.down*speed;
        // go.GetComponent<Rigidbody2D>().AddForce(Vector2.down*speed);
        // upperScreenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;
        // lowerScreenBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        // print(lowerScreenBound+" lower");
        
    }

    // Update is called once per frame
    void Update()
    {
        // go.transform.position += Time.deltaTime*speed*Vector3.down;
        // if (go.transform.position.y + 3 < lowerScreenBound)
        // {
        //     go.transform.position = new Vector3(go.transform.position.x, 12, 0) ;
        //     go.transform.localScale = new Vector3(Random.Range(lowerScaleRange, upperScaleRange), go.transform.localScale.y, 1);
        //     renderer.enabled = true;
        //     collider.enabled = true;
        // }
    }


    public void wasShot()
    {
        renderer.enabled = false;
        collider.enabled = false;
    }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (GetComponent<LowerScreenBound>() != null)
    //     {
    //         bp.relocateLowestBrick();
    //     }
    // }
}
