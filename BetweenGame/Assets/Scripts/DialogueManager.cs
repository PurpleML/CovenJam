using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text lineText;

    public Animator dialogueAnimator;

    private Queue<string> lines;

    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpeaking( Dialogue dialogue)
    {
        lines.Clear();
        dialogueAnimator.SetBool("inDialogue",true);
        nameText.text = dialogue.speaker;
        foreach(string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextLine();
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
            lineText.text += letter;
            yield return null;
        }
    }

    public void EndSpeaking()
    {
        dialogueAnimator.SetBool("inDialogue", false);
    }
}
