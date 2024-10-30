using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Npc1 : NpcCtrl, ITalkable
{
    [SerializeField] private Dailog dialogText;
    [SerializeField] private DialogueCtrl _dialogueCtrl;
    [SerializeField] private TestControl testCtrl;

    private void Awake()
    {
        _dialogueCtrl.SetFollowDialogue();
    }
    public override void Interact()
    {
        Talk(dialogText);
        
    }
    public void Talk(Dailog dialogText)
    {
        _dialogueCtrl.DisplayDialogue(dialogText);
        if(_dialogueCtrl.IsEndDialongue()) testCtrl.SetMoveable(true);
    }
    public override void DisableInteract()
    {
        _dialogueCtrl.CloseDialogue();
    }

}
