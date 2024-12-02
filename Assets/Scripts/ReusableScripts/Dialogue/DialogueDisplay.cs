using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogText;
    [SerializeField] private DialogueCtrl _dialogueCtrl;
    
    private void Start()
    {
        ShowDialouge(dialogText);
    }
    public void ShowDialouge(DialogueSO text)
    {
        _dialogueCtrl.DisplayDialogue(text);
    }
}
