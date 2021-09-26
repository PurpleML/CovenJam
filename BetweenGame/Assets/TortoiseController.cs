using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortoiseController : MonoBehaviour
{
    [SerializeField] public float speed;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public GameObject bigTortoise;

    private int horz;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        horz = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horz * speed, 0.0f);

        if(rb.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        Rigidbody2D bigTortoiseRb = bigTortoise.GetComponent<Rigidbody2D>();
        bigTortoiseRb.transform.position = new Vector3(rb.transform.position.x, bigTortoiseRb.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            if (horz == 1)
                horz = -1;
            else
                horz = 1;
        }
    }
}
