using System.Collections.Generic;
using UnityEngine;

public class TriggerActionCtrl : MonoBehaviour
{
    /*    private void Awake()
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
                    triggerAndAction.Add(trigger, action);
                }
            }
        }

        public Dictionary<Trigger, Action> triggerAndAction = new Dictionary<Trigger, Action>();

    }
    */

    private void Awake()
    {
        LoadComponents();

    }
    void LoadComponents()
    {
        {
            foreach (Transform t in transform)
            {
                Trigger trigger = t.GetComponentInChildren<Trigger>();
                Action action = t.GetComponentInChildren<Action>();
                if (trigger != null && action != null && !triggerAndAction.ContainsKey(trigger))
                {
                    triggerAndAction.Add(trigger, action);
                }
            }
            DictionaryToList();
        }
    }
    void DictionaryToList()
    {
        listPair.Clear();
        foreach (var pair in triggerAndAction)
        {
            listPair.Add(new TriggerActionPair { trigger = pair.Key, action = pair.Value });
        }
    }
    public List<TriggerActionPair> listPair = new List<TriggerActionPair>();
    public Dictionary<Trigger, Action> triggerAndAction = new Dictionary<Trigger, Action>();

}

[System.Serializable]
public struct TriggerActionPair
{
    public Trigger trigger;
    public Action action;
}
