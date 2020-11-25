using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    public Renderer renderer;

    public BoxCollider2D collider;
    
    public void wasShot()
    {
        renderer.enabled = false;
        collider.enabled = false;
    }

}
