using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    // Component Variables
    [HideInInspector] public HealthBarBehaviour healthBarScript;
    [HideInInspector] public Text gameOverText;
    [HideInInspector] public Button restartButton;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    AudioSource audioSource;

    // Variables
    public float speed;
    public float jumpForce;
    public bool canMove;
    public bool isGrounded;
    public float hurtBounce;
    public float horizontalInput;
    public float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        HurtNoMore();
        Death();
        checkPlayerBoundary();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collided is enemy, bounce after the gameobject destroyed. (Destroyed in Enemy.cs)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 4);
        }

        // Ground Check
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (healthBarScript.curHp > 0)
        {
            canMove = false; // To prevent moving when get hurt.
            isGrounded = false; // To prevent jumping when get hurt.
            // If collide from the left side, bounce left and lose health.
            if (collision.gameObject.name == "Left Collider")
            {
                rb.velocity = new Vector2(-hurtBounce, hurtBounce);
                animator.SetBool("isHurt", true);
                healthBarScript.audioSource.Play(); // Hurt Sound
                healthBarScript.curHp--;
            }
            // If collide from the right side, bounce right and lose health.
            else if (collision.gameObject.name == "Right Collider")
            {
                rb.velocity = new Vector2(hurtBounce, hurtBounce);
                animator.SetBool("isHurt", true);
                healthBarScript.audioSource.Play(); // Hurt Sound
                healthBarScript.curHp--;
            }
        }
    }

    void Move()
    {
        if (canMove)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

            // If goes right, face right.
            if (horizontalInput == 1)
            {
                sr.flipX = false;
            }
            // If goes left, face left.
            else if (horizontalInput == -1)
            {
                sr.flipX = true;
            }
        }
    }

    void Jump()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput > 0 && isGrounded != false)
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.Play(); // Jump sound.
        }
    }

    void HurtNoMore()
    {
        if (isGrounded)
        {
            canMove = true;
            animator.SetBool("isHurt", false);
        }
    }

    void Death()
    {
        if (healthBarScript.curHp <= 0)
        {
            Destroy(GetComponent<BoxCollider2D>()); // So it can pass through objects.
            Destroy(gameObject, 3f); // Delete the object out of the scene.
            gameOverText.gameObject.SetActive(true); // Show game over text.
            restartButton.gameObject.SetActive(true); // Show restart button.
        }
    }

    // If player falls.
    void checkPlayerBoundary()
    {
        if (transform.position.y < -7)
        {
            healthBarScript.curHp = 0;
        }
    }
}
