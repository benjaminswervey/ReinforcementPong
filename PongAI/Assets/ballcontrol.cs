using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontrol : MonoBehaviour
{
    private float XVel;
    private float YVel;
    private float XPos;
    private float YPos;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-5, 5), -5);
        rb.position = new Vector2(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * 5;
        XPos = rb.position.x;
        YPos = rb.position.y;
        XVel = rb.velocity.x;
        YVel = rb.velocity.y;
        /*
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
        }*/
    }
    public float ReturnXVel()
    {
        return XVel;
    }
    public float ReturnYVel()
    {
        return YVel;

    }
    public float ReturnXPos()
    {
        return XPos;
    }
    public float ReturnYPos()
    {
        return YPos;
    }
    public void NewEpisode()
    {
        rb.position = new Vector2(0, 3);
        rb.velocity = new Vector2(Random.Range(-5, 5), -5);
    }
}
