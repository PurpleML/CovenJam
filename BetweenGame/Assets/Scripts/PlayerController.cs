using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;

    public GameObject groundCheck;

    public Vector2 groundCheckPoint;
    public float groundCheckRadius;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    // separate variables for use in animation manager
    private float movementHor;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementHor = Input.GetAxisRaw("Horizontal");
        // mirror sprite to keep things simple
        if(movementHor < 0)
        {
            sprite.flipX = true;
        }
        //only un-mirror if actively going other way
        else if(movementHor > 0)
        {
            sprite.flipX = false;
        }
        //set variables used in animator
        animator.SetFloat("moveHor", Mathf.Abs(movementHor));   // this one is absolute since sprite flipping is done here

        bool grounded = groundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // FixedUpdate is called once per time step
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementHor * movementSpeed, rb.velocity.y);

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundLayer);
        //Vector2 relativeCheckPoint = new Vector2(transform.position.x, transform.position.y) + groundCheckPoint;
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(relativeCheckPoint, groundCheckRadius);
        bool grounded = groundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));
        //for (int i = 0; i < colliders.Length; i++)
        //{
            //if (colliders[i].gameObject.CompareTag("Floor"))
            if(grounded)
            {
                Debug.Log("grounded!");
                isJumping = false;

            }
        //}
        animator.SetBool("isJumping", isJumping);
    }
}
