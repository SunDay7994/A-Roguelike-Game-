using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask blockingLayer;
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    static Animator anim;
    static int playerHP;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerHP = GameController.instance.playerHP;
    }


    // Update is called once per frame
    void Update()
    {
        if (Timer.input == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector3.up);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector3.down);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                PlayerHit();
            }
        }

    }

    private void Move(Vector3 dir)
    {
        RaycastHit2D hit;

        Vector3 start = transform.position;
        Vector3 end = start + dir;

        boxCollider2D.enabled = false;
        hit = Physics2D.Linecast (start, end, blockingLayer);
        boxCollider2D.enabled = true;

        if (hit.transform == null)
        {
            transform.Translate(dir);
        }
    }

    public static void LoseHP(int loss)
    {
        anim.SetTrigger("playerChop");
        playerHP -= loss;
        if (playerHP <= 0)
        {
            GameController.instance.GameOver();
        }
    }

    public void PlayerHit()
    {
        anim.SetTrigger("playerHit");
        Select();
    }

    /* private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            if (Timer.input == true)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    PlayerHit();
                    other.GetComponent<EnemyController>().enemyHP -= 1;
                }
            }
        }
    } */

    public void Select()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < enemies.Length; i++)
        {
            float diss = Vector3.Distance(transform.position, enemies[i].transform.position);
            //float angle = Vector3.Angle(transform.position, enemies[i].transform.position - transform.position);
            if (diss < 2)
            {
                tempList.Add(enemies[i]);
            }
        } 

        foreach (var objects in tempList)
        {
            objects.GetComponent<EnemyController>().LoseHP(1);
        }
    }
}
