using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float scareDistance;

    public GameObject player;
    public GameObject bigRat;

    private Rigidbody2D rb;
    private bool scurrying;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scurrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.1)
        {
            scurrying = false;
            anim.SetBool("isScurrying", false);
        }
        if (Vector2.Distance(player.transform.position, rb.transform.position) < scareDistance && scurrying == false)
        {
            if (player.transform.position.x < rb.transform.position.x - 0.1f) {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                rb.velocity = new Vector2(speed, 0.0f);
            } else if (player.transform.position.x > rb.transform.position.x + 0.1f) {
                rb.velocity = new Vector2(-speed, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            } else {
                rb.velocity = new Vector2(Random.Range(-1f, 1f)*speed, 0.0f);
                if(rb.velocity.x < 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            scurrying = true;
            anim.SetBool("isScurrying", true);

        }
        Rigidbody2D bigRatRb = bigRat.GetComponent<Rigidbody2D>();
        bigRatRb.transform.position = new Vector3(rb.transform.position.x, bigRatRb.transform.position.y);
    }
}
