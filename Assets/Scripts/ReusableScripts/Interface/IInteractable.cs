using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    GameObject PlayerGameObject { get; set; }
    bool IsInteractable { get; set; }
    public void Interact();
}
