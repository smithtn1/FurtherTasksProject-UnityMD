using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class Car_Yaw : MonoBehaviour
{

void Move()
    {
        if (Input.GetKey("e"))
        {
            transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(new Vector3(0, -60, 0) * Time.deltaTime);
        }
    }
    
void Update ()
    {
        Move();
    }
    
}
