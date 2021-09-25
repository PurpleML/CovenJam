using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// just the name for teleporters, basically

public class Mushroom : MonoBehaviour
{

    [SerializeField] private Mushroom targetShroom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // I've done some really janky stuff here, but I'm just trynna get an idea out

    public void setTargetShroom(Mushroom newShroom)
    {
        targetShroom = newShroom;
    }

    public Vector2 getTargetShroomLocation()
    {
        return targetShroom.gameObject.transform.position;
    }
}
