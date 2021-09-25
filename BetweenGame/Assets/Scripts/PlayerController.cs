using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    // separate variables for use in animation manager
    private float movementHor;
    private float movementVer;

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
        movementVer = Input.GetAxisRaw("Vertical");

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
        //set variables used in animator controller
        animator.SetFloat("moveHor", Mathf.Abs(movementHor));
        animator.SetFloat("moveVer", movementVer);
    }

    // FixedUpdate is called once per time step
    void FixedUpdate()
    {
        // clamp to prevent faster diag movement then multiply by speed
        Vector2 movementVector = Vector2.ClampMagnitude(new Vector2(movementHor, movementVer), 1);
        rb.velocity = movementVector * movementSpeed;   // using rb for movement to get easier collisions
    }
}
