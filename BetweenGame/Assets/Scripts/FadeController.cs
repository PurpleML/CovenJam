using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public GameObject FadeScreen;
    public void FadeOut() {
        Debug.Log("Fadeout");
        FadeScreen.GetComponent<Animation>().Play("fadeout");
    }
}
