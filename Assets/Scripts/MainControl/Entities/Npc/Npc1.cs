using UnityEngine;

public class Npc1 : Npc, ITalkable
{
    [SerializeField] private DialogueSO dialogText;
    [SerializeField] private DialogueCtrl _dialogueCtrl;
    [SerializeField] private TriggerWithPlayer _triggerWithPlayer;
    [SerializeField] private SpriteRenderer _targetSprite;

    private Vector2 rangeInteract = new Vector2(2f, 3f);
    private Vector2 rangeInteractOffset = new Vector2(0, -1f);
    protected override void LoadRangeTriggers()
    {
        _triggerWithPlayer.SetRadiusTrigger(rangeInteract, rangeInteractOffset);
        _targetSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        //_dialogueCtrl.SetFollowDialogue();
    }
    protected override void LoadInteractImage()
    {
        if (_targetSprite.gameObject.activeSelf && !_isTalkable)
        {
            _targetSprite.gameObject.SetActive(false);
        }
        else if (!_targetSprite.gameObject.activeSelf && _isTalkable)
        {
            _targetSprite.gameObject.SetActive(true);
        }
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
