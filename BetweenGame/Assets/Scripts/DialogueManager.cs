using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text lineText;

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
        nameText.text = dialogue.speaker;
        foreach(string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if(lines.Count == 0)
        {
            EndSpeaking();
            return;
        }
        string line = lines.Dequeue();
        lineText.text = line;
    }

    public void EndSpeaking()
    {

    }
}
