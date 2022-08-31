using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public float[] Weights = new float[180];
    public int[] OldXVect = new int[180];
    public int[] XVect= new int[180];
    private bool terminal = false;
    private Rigidbody2D rb;
    private float Xpos;
    private GameObject Ball;
    private float R = 0;
    private int A = 0;
    private int APrime = 0;
    private float ALPHA = 0.05f;
    private float GAMMA = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        A = 0;
        APrime = 0;
        OldXVect = UpdateStateVect(A);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {     
       //while (Ball.GetComponent<Rigidbody2D>().position.y > -5)
        //{
            rb.velocity = new Vector3((1 - 2 * A)*15,0,0);//take action
        //Debug.Log(rb.velocity);
        //Debug.Log(Weights[0]);
            if(Ball.GetComponent<Rigidbody2D>().position.y > -5)
            {
                R = 1;
            }//see reward
            else
            {
                R = 0;
            }
            XVect = UpdateStateVect(A);//new (current) State Vector
            if (Ball.GetComponent<Rigidbody2D>().position.y < -5)
            {
                Weights=UpdateWeights(ALPHA, OldXVect, Weights, R);
            Ball.GetComponent<ballcontrol>().NewEpisode();
                this.transform.parent.transform.GetComponent<PaddleLord>().Kill();
            }
            APrime = ChooseAction();
       // Debug.Log(APrime);
            Weights = UpdateWeights(ALPHA, OldXVect, Weights, R,GAMMA,XVect);
            OldXVect = XVect;
            A = APrime;
        //}
    }

    public void SetXVect(int[] NewXVect)
    {
        XVect = NewXVect;
    }
    public int[] GetXVect()
    {
        return XVect;
    }
    public void SetWeight(float[] NewWeight)
    {
        Weights = NewWeight;
    }
    public float[] GetWeight()
    {
        return Weights;
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
    private int[] UpdateStateVect(int Action)
    {
        int[] VectX=new int[180];
        //XVect.Clear();
        //XVect.ensureCapacity(100);
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
        //Debug.Log(XVect.Length);
        
        VectX[(int)(Mathf.RoundToInt(((Xpos + 3.66667f) / .98f) - .5f)) + 6 * Action] = 1;
        VectX[(int)(Mathf.RoundToInt(((Xpos + 2.9333f) / .98f) - .5f)) + 6 * Action + 12] = 1;
        VectX[(int)(Mathf.RoundToInt(((Xpos + 2.2f) / .98f) - .5f)) + 6 * Action + 24] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallPosX + 5f) / 1.334f) - .5f)) + 6 * Action + 36] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallPosX + 4f) / 1.334f) - .5f)) + 6 * Action + 48] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallPosX + 3.0f) / 1.334f) - .5f)) + 6 * Action + 60] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallPosY + 8.3333f) / 2.223f) - .5f)) + 6 * Action + 72] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallPosY + 6.6665f) / 2.223f) - .5f)) + 6 * Action + 84] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallPosY + 5f) / 2.223f) - .5f)) + 6 * Action + 96] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallVelX + 8.3333f) / 2.223f) - .5f)) + 6 * Action + 108] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallVelX + 6.6665f) / 2.223f) - .5f)) + 6 * Action + 120] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallVelX + 5f) / 2.223f) - .5f)) + 6 * Action + 132] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallVelY + 8.3333f) / 2.223f) - .5f)) + 6 * Action + 144] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallVelX + 6.6665f) / 2.223f) - .5f)) + 6 * Action + 156] = 1;
        VectX[(int)(Mathf.RoundToInt(((BallVelX + 5f) / 2.223f) - .5f)) + 6 * Action + 168] = 1;
        
        return VectX;
    }
    private int ChooseAction(int[] StateVector)
    {
        
        //0=right
        //1=left
        if(Random.Range(0f,1f)>0.1f) 
        {
            //Debug.Log("choice");
            float QRight = 0;
            float QLeft = 0;
            int[] StateVectorRight=UpdateStateVect(0);
            QRight = MultiplyVectors(StateVectorRight, Weights);
           // Debug.Log("right");
           // Debug.Log(QRight);
           int[] StateVectorLeft = UpdateStateVect(1);
            QLeft = MultiplyVectors(StateVectorLeft, Weights);
           // Debug.Log("left");
           // Debug.Log(QLeft);
            if (QRight > QLeft)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return Random.Range(0,2);
        }
    }
    private int ChooseAction()
    {

        //0=right
        //1=left
        if (Random.Range(0f, 1f) > 0.1f)
        {
            //Debug.Log("choice");
            float QRight = 0;
            float QLeft = 0;
            int[] StateVectorRight = UpdateStateVect(0);
            QRight = MultiplyVectors(StateVectorRight, Weights);
            Debug.Log("right");
            Debug.Log(QRight);
            int[] StateVectorLeft = UpdateStateVect(1);
            QLeft = MultiplyVectors(StateVectorLeft, Weights);
            Debug.Log("left");
            Debug.Log(QLeft);
            if (QRight > QLeft)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return Random.Range(0, 2);
        }
    }
    private float MultiplyVectors(int[] A, float[] B)
    {
       float total = 0;
        if (A.Length!=B.Length)
        {
            return -999;
        }
        else
        {
            for(int i = 0; i < A.Length; i++)
            {
                total = total + (float)A[i] * (float)B[i];
            }
            return total;
        }

    }
    private float[] UpdateWeights(float alpha, int[]  State, float[] Weight, float R)
    {
        float q= MultiplyVectors(State, Weight);
        for (int i=0;i< Weight.Length; i++)
        {
            Weight[i] = (float)Weight[i] + alpha * (R - q) * (float)State[i];
        }
        return Weight;
    }
    private float[] UpdateWeights(float alpha, int[] State, float[] Weight, float R,float gamma, int[] StatePrime)
    {
        float q = MultiplyVectors(State, Weight);
        float qPrime = MultiplyVectors(StatePrime, Weight);
        for (int i = 0; i < Weight.Length; i++)
        {
            Weight[i] = (float)Weight[i] + alpha * (R + gamma*qPrime-q) * (float)State[i];
        }
        return Weight;
    }
}
