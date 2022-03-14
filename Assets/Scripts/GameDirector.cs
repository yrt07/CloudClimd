using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public  float gametimer;
    //private GameObject touchflag;
    private GameObject clearTime;
     //private int timeup;

    // Start is called before the first frame update
    void Start()
    {
        clearTime = GameObject.Find("Timer");
        //touchflag = GameObject.Find("Flag");
        //timeup = 1;
    }
    public float getgametimer()
    {
        return gametimer;
    }

    // Update is called once per frame
    void Update()
    {
        gametimer += Time.deltaTime;  //* timeup;
        clearTime.GetComponent<Text>().text =  gametimer.ToString("F2") + "•b";
        
      /* if(touchflag)
        {
            timeup = 0;
            Debug.Log("‚ ");
        }*/
    }
}
