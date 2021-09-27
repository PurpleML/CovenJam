using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleUnPause : MonoBehaviour
{
    public GameObject Tortoise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fdsa" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = Tortoise.GetComponent<Rigidbody2D>();
            if (rb.velocity == Vector2.zero && Tortoise.GetComponent<TortoiseController>().isDone != true)
            {
                rb.isKinematic = false;
                Tortoise.GetComponent<TortoiseController>().speed = 1.2f;
                rb.velocity = new Vector2(1.2f, 0);
            }
        }
    }
}
