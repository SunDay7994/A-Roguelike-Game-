using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask blockingLayer;
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    Animator anim;
    Vector2 move; 
    Vector3 pos;
    public bool isStop;
    public static PlayerController instance;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        if (Timer.input == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                RaycastHit2D hit;

                Vector3 start = transform.position;
                Vector3 end = start + Vector3.up;

                boxCollider2D.enabled = false;
                hit = Physics2D.Linecast (start, end, blockingLayer);
                boxCollider2D.enabled = true;

                if (hit.transform == null)
                {
                    transform.Translate(Vector3.up);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                RaycastHit2D hit;

                Vector3 start = transform.position;
                Vector3 end = start + Vector3.down;

                boxCollider2D.enabled = false;
                hit = Physics2D.Linecast (start, end, blockingLayer);
                boxCollider2D.enabled = true;

                if (hit.transform == null)
                {
                    transform.Translate(Vector3.down);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                RaycastHit2D hit;

                Vector3 start = transform.position;
                Vector3 end = start + Vector3.left;

                boxCollider2D.enabled = false;
                hit = Physics2D.Linecast (start, end, blockingLayer);
                boxCollider2D.enabled = true;

                if (hit.transform == null)
                {
                    transform.Translate(Vector3.left);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {

                Vector3 start = transform.position;
                Vector3 end = new Vector3(start.x + 1, start.y, start.z);

                boxCollider2D.enabled = false;
                bool hit = Physics2D.Linecast (start, end, blockingLayer);
                boxCollider2D.enabled = true;

                if (!hit)
                {
                    transform.Translate(Vector3.right);
                }
            }
        }

    }

    /* public void Stop()
    {
        isStop = true;
    }

    public void nStop()
    {
        isStop = false;
    } */

    
}
