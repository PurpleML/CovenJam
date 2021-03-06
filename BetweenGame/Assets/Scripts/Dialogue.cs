using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Sprite speakerHead;
    public string speaker;
    public AudioSource speakerChatter;
    public float speakingSpeed;

    [TextArea(3, 10)]
    public string[] lines;
}
