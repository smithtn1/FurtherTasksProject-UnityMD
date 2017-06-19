using UnityEngine;
using System.Collections;

public class Car_Movement : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        rb.AddForce((movement * speed));
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("there");
            Vector3 newMove = new Vector3(0, 0.0f, 0);
            //rb.velocity(newMove);
        }
        
    }
}
