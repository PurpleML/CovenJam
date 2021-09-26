using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{
    [SerializeField] private float crumbleTimer;
    [SerializeField] private float regrowTimer;
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
            StartCoroutine(CrumbleAway());
        }
    }

    IEnumerator CrumbleAway()
    {
        yield return new WaitForSeconds(crumbleTimer);
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(regrowTimer);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //gameObject.SetActive(true);
    }
}
