using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeaker : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartSpeaking(dialogue);
    }
}
