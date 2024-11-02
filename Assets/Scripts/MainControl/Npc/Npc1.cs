using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Npc1 : NpcCtrl, ITalkable
{
    [SerializeField] private DialogueSO dialogText;
    [SerializeField] private DialogueCtrl _dialogueCtrl;

    private void Awake()
    {
        _dialogueCtrl.SetFollowDialogue();
    }
    public override void Interact()
    {
        Talk(dialogText);
        
    }
    public void Talk(DialogueSO dialogText)
    {
        _dialogueCtrl.DisplayDialogue(dialogText);
    }
    public override void DisableInteract()
    {
        _dialogueCtrl.CloseDialogue();
    }

}
