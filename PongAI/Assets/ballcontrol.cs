using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKey("up"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.up*2);
        }else if (Input.GetKey("down"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 2);
        }
        
        if (Input.GetKey("left"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 2);
        }
        else if (Input.GetKey("right"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 2);
        }
    }
}
