using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask blockingLayer;
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    Animator anim;
    public int enemyHP;
    public enum Direction { up, down, left, right };
    public Direction direction;
    public float timer = 0.6f;

    // Start is called before the first frame update

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyHP = 2;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        { 
            timer = 0.6f;
            EnemyMove();
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

    public void EnemyMove()
    {
        direction = (Direction)Random.Range(0, 4);

        switch(direction)
            {
                case Direction.up:
                    Move(Vector3.up);
                    break;
                case Direction.down:
                    Move(Vector3.down);
                    break;
                case Direction.left:
                    Move(Vector3.left);
                    break;
                case Direction.right:
                    Move(Vector3.right);
                    break;
            }
    }

    public void LoseHP(int loss)
    {
        enemyHP -= loss;
        if (enemyHP <= 0)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void EnemyHit()
    {
        PlayerController.LoseHP(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyHit();
        }
    }
}
