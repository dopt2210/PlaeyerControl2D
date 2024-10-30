using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "NPC Dialogue/Dialogue")]
public class Dailog : ScriptableObject
{
    public string speakerName;
    [TextArea(5,10)]
    public string[] speakerDialogue;
}
