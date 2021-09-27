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
    private bool onPillow;

    public bool bouncing;

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
        nearNPC = null;
        onPillow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMovement && !dead)
        {
            //Camera cam = FindObjectOfType<Camera>();
            //if (grounded)
            //{
            //    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, Time.deltaTime * 3);
            //    Vector3 camOffse
            //    cam.gameObject.GetComponent<CameraTrack>().SetOffset(Mathf.Lerp(, 5, Time.deltaTime * 3));
            //}
            //else
            //{
            //    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7.5f, Time.deltaTime);
            //}
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

            // INTERACTING WITH A MUSHROOM or pillow
            if (Input.GetKeyDown(KeyCode.S) && (touchingShroom != null || onPillow))
            {
                rb.velocity = Vector2.zero;
                animator.SetFloat("moveHor", 0);
                animator.SetBool("isJumping", false);
                StartCoroutine(Sleep());
            }

            //ANIMATOR CONTROL
            animator.SetFloat("moveHor", Mathf.Abs(movementHor));
            animator.SetBool("isJumping", !grounded);

            //CHECK FOR DIALOGUE INTERACT
            if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.E)) && nearNPC != null)
            {
                inMovement = false;
                rb.velocity = Vector2.zero;
                animator.SetFloat("moveHor",  0);
                animator.SetBool("isJumping", false);
                FindObjectOfType<DialogueManager>().StartSpeaking(nearNPC.GetComponent<NPCSpeaker>().dialogue);
            }

        }
        else if (!dead)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                if (!FindObjectOfType<DialogueManager>().DisplayNextLine())
                {
                    inMovement = true;
                }
            }
        }
    
    }

    private void EndJumpHold()
    {
        jumpHold = false;
    }

    private void TeleportToShroom()
    {
        Vector2 teleToPos = touchingShroom.getTargetShroomLocation();
        Vector3 teleTo3 = new Vector3(teleToPos.x, teleToPos.y-.6f, transform.position.z);
        transform.position = teleTo3;
    }

    IEnumerator Sleep()
    {
        animator.SetBool("isSleeping", true);
        inMovement = false;
        yield return new WaitForSeconds(1.5f);

        if (!onPillow)
        {
            GetComponent<FadeController>().FadeShroom();
            Invoke("TeleportToShroom", 0.5f);
            yield return new WaitForSeconds(.7f);
            animator.SetBool("isSleeping", false);
            animator.SetBool("isWaking", true);
            yield return new WaitForSeconds(.75f);
            animator.SetBool("isWaking", false);
            inMovement = true;
        }
        else
        {
            Debug.Log("Level Over");
            GetComponent<FadeController>().FadeShroom();
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
            bouncing = false;
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
        Invoke("RestartLevel", 2);
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
        else if (collision.gameObject.CompareTag("Pillow"))
        {
            onPillow = true;
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
        else if (collision.gameObject.CompareTag("Pillow"))
        {
            onPillow = false;
        }
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
}
