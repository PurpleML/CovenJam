using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private float jumpHoldSpeed;
    [SerializeField] private float jumpHoldDuration;

    private Rigidbody2D body;
    private bool grounded;
    private bool jumpHold;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        grounded = false;
        jumpHold = false;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space)) {
            if (grounded) {
                body.velocity += new Vector2(0.0f, jumpspeed);
                grounded = false;
                jumpHold = true;
                Invoke("EndJumpHold", jumpHoldDuration);
            } else if (jumpHold) {
                body.velocity += new Vector2(0.0f, jumpHoldSpeed);
            }
        }
    }

    private void EndJumpHold()
    {
        jumpHold = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}
