using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueCtrl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NpcName;
    [SerializeField] TextMeshProUGUI NpcDialogue;
    [SerializeField] Transform body;
    [SerializeField] float typeSpeed = 10f;

    Coroutine dialogueCoroutine;
    private Queue<string> dialogues = new Queue<string>();
    const float MAX_TYPE_TIME = 0.01f;
    string d;
    bool _isTyping;
    bool _isEnd;
    #region Display
    public void DisplayDialogue(DialogueSO dialogueText)
    {
        gameObject.SetActive(true);
        if (dialogues.Count == 0)
        {
            if (!_isEnd)
            {
                StartDialogue(dialogueText);
            }
            else if(_isEnd && !_isTyping)
            {
                FinishDialogue();
                return;
            }

        }
        if (!_isTyping)
        {
            d = dialogues.Dequeue();
            dialogueCoroutine = StartCoroutine(TypeDialogueCoroutine(d));
        }
        else
        {
            StopTypeEarly(d);
        }
        if (dialogues.Count == 0)
        {
            _isEnd = true;
        }
    }
    private void FinishDialogue()
    {
        _isEnd = false;
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
    void StopTypeEarly(string p)
    {
        StopCoroutine(dialogueCoroutine);
        NpcDialogue.maxVisibleCharacters = p.Length;
        _isTyping = false;
    }
    private void StartDialogue(DialogueSO dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        NpcName.text = dialogueText.speakerName;
        for (int i = 0; i < dialogueText.speakerDialogue.Length; i++)
        {
            dialogues.Enqueue(dialogueText.speakerDialogue[i]);
        }
    }
    IEnumerator TypeDialogueCoroutine(string p)
    {
        _isTyping = true;

        int maxVisibleChars = 0;

        NpcDialogue.text = p;
        NpcDialogue.maxVisibleCharacters = maxVisibleChars;

        foreach (char c in p.ToCharArray())
        {

            maxVisibleChars++;
            NpcDialogue.maxVisibleCharacters = maxVisibleChars;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        _isTyping = false;
    }
    #endregion
    #region Check
    public bool IsDialogueActive()
    {
        return gameObject.activeSelf;
    }
    public void CloseDialogue()
    {
        gameObject.SetActive(false);
    }
    public bool IsEndDialongue() => _isEnd;

    public void SetFollowDialogue()
    {
        transform.position = body.position + new Vector3(0,5,0);
    }
    #endregion
}

