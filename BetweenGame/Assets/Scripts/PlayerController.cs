using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private float jumpHoldSpeed;
    [SerializeField] private float jumpHoldDuration;
    public GameObject deathParticles;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool jumpHold;
    private bool grounded;
    private bool inMovement;
    private bool dead;
    private Mushroom touchingShroom;
    private GameObject nearNPC;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();;
        animator = GetComponent<Animator>();
        jumpHold = false;
        grounded = false;
        dead = false;
        inMovement = true;
        touchingShroom = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMovement && !dead)
        {
            // MOVEMENT
            float movementHor = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(movementHor * speed, rb.velocity.y);
            if (movementHor != 0)
            {
                sprite.flipX = movementHor > 0 ? false : true;
            }

            // JUMPING
            if (Input.GetButtonDown("Jump") && grounded)
            {
                if (grounded)
                {
                    rb.velocity += new Vector2(0.0f, jumpspeed);
                    grounded = false;
                    jumpHold = true;
                    Invoke("EndJumpHold", jumpHoldDuration);
                }
                else if (jumpHold)
                {
                    rb.velocity += new Vector2(0.0f, jumpHoldSpeed);
                }
            }

            // INTERACTING WITH A MUSHROOM
            if (Input.GetKeyDown(KeyCode.S) && touchingShroom != null)
            {
                Vector2 teleToPos = touchingShroom.getTargetShroomLocation();
                Vector3 teleTo3 = new Vector3(teleToPos.x, teleToPos.y, transform.position.z);
                transform.position = teleTo3;
            }

            //ANIMATOR CONTROL
            animator.SetFloat("moveHor", Mathf.Abs(movementHor));
            animator.SetBool("isJumping", !grounded);
        }
    
    }

    private void EndJumpHold()
    {
        jumpHold = false;
    }

    // FixedUpdate is called once per time step
    void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        else if (collision.gameObject.CompareTag("Mushroom"))
        {
            touchingShroom = collision.gameObject.GetComponent<Mushroom>();
        }
        else if (collision.gameObject.CompareTag("Nightmare"))
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y, -10), transform.rotation);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<FadeController>().FadeOut();
        dead = true;
        Invoke("RestartLevel", 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            touchingShroom = collision.gameObject.GetComponent<Mushroom>();
        }
        else if (collision.gameObject.CompareTag("NPC"))
        {
            nearNPC = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            touchingShroom = null;
        }
        else if (collision.gameObject.CompareTag("NPC"))
        {
            nearNPC = null;
        }
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
}
