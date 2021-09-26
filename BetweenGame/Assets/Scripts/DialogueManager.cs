using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text lineText;
    public Image speakerImage;

    public Animator dialogueAnimator;

    private Dialogue dialogue;
    private Queue<string> lines;

    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<string>();
        dialogue = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpeaking( Dialogue dialogue)
    {
        lines.Clear();
        dialogueAnimator.SetBool("inDialogue",true);
        speakerImage.sprite = dialogue.speakerHead;
        nameText.text = dialogue.speaker;
        foreach(string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextLine();
        this.dialogue = dialogue;
    }

    public bool DisplayNextLine()
    {
        if(lines.Count == 0)
        {
            EndSpeaking();
            return false;
        }
        string line = lines.Dequeue();
        //lineText.text = line;
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
        return true;
    }

    IEnumerator TypeLine (string line)
    {
        lineText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogue.speakerChatter.pitch = Random.Range(.9f, 1.1f);
            dialogue.speakerChatter.Play();
            lineText.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    public void EndSpeaking()
    {
        dialogueAnimator.SetBool("inDialogue", false);
    }
}
