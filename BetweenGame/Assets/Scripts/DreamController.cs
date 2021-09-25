using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamController : MonoBehaviour
{

    void Start() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void setCollisions(bool collisions)
    {
        Debug.LogError("called");
        GetComponent<BoxCollider2D>().enabled = collisions;
    }
}
