using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Vector2 move; 
    Vector3 pos;
    public bool isStop;
    public bool up, down, left, right;
    public static PlayerController instance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(move.x, move.y, 0);

        if(isStop)
        {
             transform.position = pos;
        }
        else
        {
        if (Input.GetKeyDown(KeyCode.W))
        {
            pos = rb.position;
            up = true;
            left = false;
            right = false;
            down = false; 
            transform.Translate(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pos = rb.position;
            up = false;
            left = false;
            right = false;
            down = true; 
            transform.Translate(Vector3.down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pos = rb.position;
            up = false;
            left = true;
            right = false;
            down = false; 
            transform.Translate(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pos = rb.position;            
            up = false;
            left = false;
            right = true;
            down = false; 
            transform.Translate(Vector3.right);
        }
        }

    }


    public void Stop()
    {
        isStop = true;
        Debug.Log("stop");
    }

    public void nStop()
    {
        isStop = false;
        Debug.Log("nstop");
    }
}
