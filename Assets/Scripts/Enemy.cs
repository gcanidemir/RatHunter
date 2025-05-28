using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Component Variables
    Rigidbody2D enemyRb;
    Animator enemyAnimator;
    SpriteRenderer enemySr;

    // Variables
    public float speed;
    public bool goRight;
    public bool isDead;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemySr = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }

    void Patrol()
    {
        if (!isDead)
        {
            if (goRight) // If going right.
            {
                enemyRb.velocity = new Vector2(speed, enemyRb.velocity.y);
                enemySr.flipX = true;
            }
            else // If going left.
            {
                enemyRb.velocity = new Vector2(-speed, enemyRb.velocity.y);
                enemySr.flipX = false;
            }
        }
    }

    void Death()
    {
        isDead = true;
        enemyAnimator.SetBool("isDead", true);
        Destroy(gameObject, 0.31f);
    }
}
