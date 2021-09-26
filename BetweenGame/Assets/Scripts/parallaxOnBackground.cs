using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxOnBackground : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;

    private Transform currentCameraTransform;
    private Vector3 lastCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentCameraTransform = Camera.main.transform;
        lastCameraPosition = currentCameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 changeInPosition = currentCameraTransform.position - lastCameraPosition;
        transform.position += changeInPosition * parallaxMultiplier;
        lastCameraPosition = currentCameraTransform.position;
    }
}
