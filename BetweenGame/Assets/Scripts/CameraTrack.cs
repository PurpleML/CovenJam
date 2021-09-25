using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class just causes the camera to follow the player rigidly in terms of x and y coordinates
public class CameraTrack : MonoBehaviour
{

    [SerializeField] private GameObject tracked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(tracked.transform.position.x, tracked.transform.position.y + 1.5f, gameObject.transform.position.z);
    }
}
