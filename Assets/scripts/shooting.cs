using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shoot();
        }
    }


    void shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
    
    
}
