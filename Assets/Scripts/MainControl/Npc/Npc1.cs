using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Npc1 : NpcCtrl, ITalkable
{
    [SerializeField] private Dailog dialogText;
    [SerializeField] private DialogueCtrl ctrl;

    public override void Interact()
    {
        Talk(dialogText);
    }

    public void Talk(Dailog dialogText)
    {
        ctrl.DisplayDialogue(dialogText);
    }
}
