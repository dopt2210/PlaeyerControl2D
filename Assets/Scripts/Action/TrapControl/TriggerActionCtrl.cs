using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TriggerActionCtrl : MonoBehaviour 
{
    Transform terrainTrap;
    private void Awake()
    {
        trigger = transform.GetComponentInChildren<Trigger>(); 
        action = transform.GetComponentInChildren<Action>();
        if(gameObject.CompareTag("TerrainTrap")) LoadComponentTrap();
    }
    void LoadComponentTrap()
    {
        terrainTrap = GameObject.FindGameObjectWithTag("TerrainTrap").transform;

        if (trigger == null) return;
        foreach(Transform t in terrainTrap )
        {
            triggers.Add(t.GetComponentInChildren<Trigger>());
        }
    }

    public Trigger trigger;

    public List<Trigger> triggers;

    public Action action;

}
