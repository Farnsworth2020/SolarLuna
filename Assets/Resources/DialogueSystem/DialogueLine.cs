using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    [TextArea]
    public string dialogue;
    [TextArea]
    public string answerText;
}
