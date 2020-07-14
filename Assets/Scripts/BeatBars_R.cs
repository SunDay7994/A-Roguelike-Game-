using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBars_R : MonoBehaviour
{
    float speed_R = 157.7083f;
    public Vector3 pos_R;
    // Start is called before the first frame update
    void Start()
    {
        pos_R = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void FixedUpdate() 
    {
        if (transform.position.x > 378.5)
        {
            transform.Translate(Vector3.left * speed_R * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = new Vector3(757, pos_R.y, pos_R.z);
        }

    }
}
