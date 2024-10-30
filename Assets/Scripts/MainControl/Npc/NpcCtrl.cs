using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public abstract class NpcCtrl : MonoBehaviour, IInteractable
{
    [SerializeField] protected SpriteRenderer _targetSprite;
    private Transform _player;
    protected float interactRange = 2f;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && IsWithinRange(interactRange))
        {
            Interact();
        }
        else if(!IsWithinRange(interactRange))
            DisableInteract();
        if (_targetSprite.gameObject.activeSelf && !IsWithinRange(interactRange))
        {
            _targetSprite.gameObject.SetActive(false);
        }
        else if(!_targetSprite.gameObject.activeSelf && IsWithinRange(interactRange))
        {
            _targetSprite.gameObject.SetActive(true);
        }
    }
    public abstract void Interact();
    public abstract void DisableInteract();
    public bool IsWithinRange(float range)
    {
        if (Vector2.Distance(_player.position, this.transform.position) < range) return true;
        else return false;
    }
}
