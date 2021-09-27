using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{ 

    [SerializeField]private float bounciness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bounce!");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().bouncing == false)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += collision.relativeVelocity * -1 * bounciness;
                Mathf.Clamp(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude, 0f, 20f);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += collision.relativeVelocity * -1;
                Mathf.Clamp(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude, 0f, 20f);
            }
                collision.gameObject.GetComponent<PlayerController>().bouncing = true;
        }
    }
}
