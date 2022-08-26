using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLord : MonoBehaviour
{
    public GameObject PaddleSlave;
    public ArrayList XVectStore = new ArrayList();
    private GameObject CurrentPaddle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        XVectStore = CurrentPaddle.GetComponent<PaddleControl>().GetXVect();
        Destroy(CurrentPaddle);
    }
    public void Birth()
    {
        CurrentPaddle = Instantiate(PaddleSlave,this.transform);
        CurrentPaddle.GetComponent<PaddleControl>().SetXVect(XVectStore);
    }
}
