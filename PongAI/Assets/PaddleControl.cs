using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    private ArrayList XVect=new ArrayList();
    private bool terminal = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
