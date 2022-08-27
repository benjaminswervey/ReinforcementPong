using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    private ArrayList XVect=new ArrayList();
    private bool terminal = false;
    private Rigidbody2D rb;
    private float Xpos;
    private GameObject Ball;
   
    private int A = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
      
    }

    public void SetXVect(ArrayList NewXVect)
    {
        XVect = NewXVect;
    }
    public ArrayList GetXVect()
    {
        return XVect;
    }
    public void SetTerminal(bool term)
    {
        terminal = term;
    }
    public bool GetTerminal()
    {
        return terminal;
    }
    public void SetBall(GameObject ball)
    {
        Ball = ball;
    }
    private ArrayList UpdateStateVect()
    {
        XVect.Clear();
        Xpos = rb.position.x;
        float BallPosX = Ball.GetComponent<ballcontrol>().ReturnXPos();
        float BallPosY = Ball.GetComponent<ballcontrol>().ReturnYPos();
        float BallVelX = Ball.GetComponent<ballcontrol>().ReturnXVel();
        float BallVelY = Ball.GetComponent<ballcontrol>().ReturnYVel();
        //SET STATE VECTOR (MAKE ITS OWN FUCTION)
        // -2.2<Paddle X<2.2
        //-3<ball x Pos<3
        //-5<ball y Pos<5
        //-5<ball x Vel<5
        //-5<ball x Vel<5
        XVect[(int)((Mathf.RoundToInt(Xpos + 3.66667f) / .98f) - .5f) + 6 * A] = 1;
        XVect[(int)((Mathf.RoundToInt(Xpos + 2.9333f) / .98f) - .5f) + 6 * A + 12] = 1;
        XVect[(int)((Mathf.RoundToInt(Xpos + 2.2f) / .98f) - .5f) + 6 * A + 24] = 1;
        XVect[(int)((Mathf.RoundToInt(BallPosX + 5f) / 1.334f) - .5f) + 6 * A + 36] = 1;
        XVect[(int)((Mathf.RoundToInt(BallPosX + 4f) / 1.334f) - .5f) + 6 * A + 48] = 1;
        XVect[(int)((Mathf.RoundToInt(BallPosX + 3.0f) / 1.334f) - .5f) + 6 * A + 60] = 1;
        XVect[(int)((Mathf.RoundToInt(BallPosY + 8.3333f) / 2.223f) - .5f) + 6 * A + 72] = 1;
        XVect[(int)((Mathf.RoundToInt(BallPosY + 6.6665f) / 2.223f) - .5f) + 6 * A + 84] = 1;
        XVect[(int)((Mathf.RoundToInt(BallPosY + 5f) / 2.223f) - .5f) + 6 * A + 96] = 1;
        XVect[(int)((Mathf.RoundToInt(BallVelX + 8.3333f) / 2.223f) - .5f) + 6 * A + 108] = 1;
        XVect[(int)((Mathf.RoundToInt(BallVelX + 6.6665f) / 2.223f) - .5f) + 6 * A + 120] = 1;
        XVect[(int)((Mathf.RoundToInt(BallVelX + 5f) / 2.223f) - .5f) + 6 * A + 132] = 1;
        XVect[(int)((Mathf.RoundToInt(BallVelY + 8.3333f) / 2.223f) - .5f) + 6 * A + 144] = 1;
        XVect[(int)((Mathf.RoundToInt(BallVelX + 6.6665f) / 2.223f) - .5f) + 6 * A + 156] = 1;
        XVect[(int)((Mathf.RoundToInt(BallVelX + 5f) / 2.223f) - .5f) + 6 * A + 168] = 1;
        return XVect;
    }
}
