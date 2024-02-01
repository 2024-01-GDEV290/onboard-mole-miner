using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D drillCollider;
    public AudioSource audio;
    public AudioClip drilling_drill;
    public AudioSource pickup;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true, wasGrounded = true;
    private bool drilling = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        wasGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {   

        // Horizontal Movement

        if (!drilling)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
        }

        // Jump (Non-functional for some reason?)

        if (Input.GetButtonDown("Jump") && IsGrounded())// && wasGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //}

        // Drill 
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Drill", true);
            drilling = true;
            horizontal = 0;
            drillCollider.enabled = true;
            audio.PlayOneShot(drilling_drill, 0.5f);
        } 

        if (Input.GetKeyUp(KeyCode.J))
        {
            animator.SetBool("Drill", false);
            drilling = false;
            drillCollider.enabled = false;
            audio.Stop();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Rock")
        {
            pickup.Play();
        }
    }
}




