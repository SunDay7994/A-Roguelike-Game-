using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBars_L : MonoBehaviour
{
    float speed_L = 157.7083f;
    //315.4167
    public Vector3 pos_L;
    // Start is called before the first frame update
    void Start()
    {
        pos_L = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void FixedUpdate() 
    {
        if (transform.position.x < 378.5)
        {
            transform.Translate(Vector3.right * speed_L * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = new Vector3(0, pos_L.y, pos_L.z);
        }

    }
}
