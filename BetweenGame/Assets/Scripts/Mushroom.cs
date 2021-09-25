using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    public Mushroom targetShroom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getTargetShroomLocation()
    {
        return targetShroom.gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touched Player");
            collision.GetComponent<PlayerController>().setShroom(gameObject.GetComponent<Mushroom>());
        }
    }
}
