using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TriggerActionCtrl : MonoBehaviour 
{
    private void Awake()
    {
        LoadComponents();
    }
    void LoadComponents()
    {
        foreach (Transform t in transform)
        {
            Trigger trigger = t.GetComponentInChildren<Trigger>();
            Action action = t.GetComponentInChildren<Action>();
            if (trigger != null && action != null)
            {
                triggers.Add(trigger);
                actions.Add(action);
            }
        }
    }


    public List<Trigger> triggers = new List<Trigger>();

    public List<Action> actions = new List<Action>();

}
