using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "NPC/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public string speakerName;
    [TextArea(5,10)]
    public string[] speakerDialogue;
}
