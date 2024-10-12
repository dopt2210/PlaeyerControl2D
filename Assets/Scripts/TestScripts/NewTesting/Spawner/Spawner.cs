using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Spawner : MonoBehaviour
{
    protected Transform prefabHolder;
    protected List<Transform> listPrefab;
    protected List<Transform> listPool;

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        GetListPrefab();
        GetListPool();
    }

    private void GetListPool()
    {
        if(prefabHolder != null) return;
        prefabHolder = GameObject.Find("_Holderable").transform;
    }

    protected virtual void GetListPrefab()
    {
        if (listPrefab.Count > 0) return;
        Transform parent = GameObject.Find("_Spawnable").transform;
        foreach (Transform tranform in parent)
        {
            listPrefab.Add(tranform);
        }
        RemoveListPrefab();
    }
    protected virtual void RemoveListPrefab()
    {
        foreach(Transform tranform in listPrefab)
        {
            tranform.gameObject.SetActive(false);
        }
    }
    protected virtual Transform Spawn(string name)
    {
        Transform prefab = GetObjectByName(name);
        if(prefab == null) return null;
        
        return this.Spawn(prefab);
    }
    protected virtual Transform Spawn(Transform transform)
    {
        Transform newPrefab = GetObjectFromPool(transform);
        newPrefab.parent = prefabHolder;
        return newPrefab;
    }


    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach(Transform transform in listPool)
        {
            if(transform.name == prefab.name)
            {
                listPool.Remove(transform);
                return transform;
            }
        }
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    public virtual void Despawn(Transform transform)
    {
        listPool.Add(transform);
        transform.gameObject.SetActive(false);
    }
    public virtual Transform GetObjectByName(string name)
    {
        foreach (Transform tranform in listPrefab)
        {
            if (name == tranform.name) return transform;
        }
        return null;
    }
    public virtual Transform RandomPrefab()
    {
        int rand = UnityEngine.Random.Range(0, listPrefab.Count);
        return listPrefab[rand];
    }
}
