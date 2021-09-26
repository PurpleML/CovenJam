using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Controller : MonoBehaviour
{
    public GameObject Tortoise;
    public GameObject Boulder;
    public GameObject brokenBoulder;
    public GameObject Boulder2;
    public GameObject brokenBoulder2;

    private SpriteRenderer sprite;
    private SpriteRenderer sprite2;
    // Start is called before the first frame update
    void Start()
    {
        sprite = brokenBoulder.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        sprite2 = brokenBoulder2.GetComponent<SpriteRenderer>();
        sprite2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asdf"+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = Tortoise.GetComponent<Rigidbody2D>();
            if (rb.position.x > 24.0f) {
                Destroy(Boulder);
                sprite.enabled = true;
                Destroy(Boulder2);
                sprite2.enabled = true;
            }  
            rb.isKinematic = true;
            Tortoise.GetComponent<TortoiseController>().speed = 0;
        }
    }
}
