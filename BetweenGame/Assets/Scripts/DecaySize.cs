using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecaySize : MonoBehaviour
{
    [SerializeField]private float decayRate;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Decay());
        }
    }

    IEnumerator Decay()
    {
        while (gameObject.transform.localScale.x > decayRate * Time.deltaTime)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - decayRate * Time.deltaTime,
                gameObject.transform.localScale.y - decayRate * Time.deltaTime,
                gameObject.transform.localScale.z - decayRate * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}