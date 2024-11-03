using UnityEngine;

public class Npc1 : Npc, ITalkable
{
    [SerializeField] private DialogueSO dialogText;
    [SerializeField] private DialogueCtrl _dialogueCtrl;
    
    [SerializeField] private TriggerWithPlayer _triggerWithPlayer;

    private Vector2 rangeInteract = new Vector2(2f, 3f);
    private Vector2 rangeInteractOffset = new Vector2(0, -1f);

    protected override void LoadRangeTriggers()
    {
        _triggerWithPlayer.SetRadiusTrigger(rangeInteract, rangeInteractOffset);
        _dialogueCtrl.SetFollowDialogue();
    }

    public override void Interact()
    {
        Talk(dialogText);
    }
    public override void DisableInteract()
    {
        _dialogueCtrl.CloseDialogue();
    }
    public void Talk(DialogueSO dialogText)
    {
        _dialogueCtrl.DisplayDialogue(dialogText);
    }

}
