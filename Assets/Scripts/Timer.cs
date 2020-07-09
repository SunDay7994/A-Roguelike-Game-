using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool pointer;
    public float interval;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake() 
    {
        timer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
        timer.AutoReset = true;
        InvokeRepeating("ChangePointer", 0, interval);
        InvokeRepeating("Reset", 0, interval);  
    }

    public int n;

    System.Timers.Timer timer =  new System.Timers.Timer(50);  
    public void ChangePointer()
    {
        timer.Start();
        if (Input.anyKey)
        {
            timer.Close();
            if (Input.anyKey)
            {
                Debug.Log("333");
            }
        } 
        Debug.Log("2333");

    }

    public void theout(object source, System.Timers.ElapsedEventArgs e)
    {
        n++;
    }

    public void Reset() 
    {
        n = 0;
    }
}
