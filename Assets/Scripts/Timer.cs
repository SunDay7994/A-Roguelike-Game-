using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 0.6f;
    float inputTime;
    static public bool multipleInput;
    static public bool input;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.anyKeyDown)
        {
            input = false;
            inputTime = timer;
            if(inputTime <= 0.3f)
            {
                input = true;
                Debug.Log("successful");
            }
        }
        
        if (timer <= 0) 
        { 
            timer = 0.6f;
        }
    }
}
