using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class just causes the camera to follow the player rigidly in terms of x and y coordinates
public class CameraTrack : MonoBehaviour
{
    [SerializeField] private GameObject tracked;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float factor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 deltaPosition = (gameObject.transform.position - offset) - tracked.transform.position;
        gameObject.transform.position -= deltaPosition * factor;
    }
}
