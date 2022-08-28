using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLord : MonoBehaviour
{
    public GameObject PaddleSlave;
    public ArrayList XVectStore = new ArrayList(180); //3 tilings, with 6 bins, of 5 variables (X paddle, X&Y pos ball, X&Y Vel Ball) times two actions Left or Right
    public ArrayList WeightStore = new ArrayList(180); //weights 
    private GameObject CurrentPaddle;
    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        XVectStore.Clear();
        WeightStore.Clear();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //-2.2 to 2.2
       
        
        

    }

    public void Kill()
    {
        XVectStore = CurrentPaddle.GetComponent<PaddleControl>().GetXVect();
        WeightStore = CurrentPaddle.GetComponent<PaddleControl>().GetWeight();
        Destroy(CurrentPaddle);
    }
    public void Birth()
    {
        CurrentPaddle = Instantiate(PaddleSlave,this.transform);
        CurrentPaddle.GetComponent<PaddleControl>().SetXVect(XVectStore);
        CurrentPaddle.GetComponent<PaddleControl>().SetBall(ball);
        CurrentPaddle.GetComponent<PaddleControl>().SetWeight(WeightStore);
    }
}
