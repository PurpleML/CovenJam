using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class simply goes on an object with a trigger collider2D
// when any object tagged with "Key" intersects it, the mushroom shroomToChange
// has its target shroom (the mushroom it teleports the player to when interacted with"
// to the mushroom shroomToChangeWith
// thus we can make little puzzles that change the mushrooms

public class Lock : MonoBehaviour
{
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject unlocked;

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
        if (collision.gameObject.Equals(key))
        {
            unlocked.SetActive(true);
            locked.SetActive(false);
        }
    }
}
