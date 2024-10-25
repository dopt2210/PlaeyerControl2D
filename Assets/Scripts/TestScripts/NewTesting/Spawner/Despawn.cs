using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : MonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        Despawning();
    }

    protected virtual void DespawnObject()
    {
        Destroy(transform.parent.gameObject);
    }
    protected virtual void Despawning()
    {
        if(!CanDestroy()) return;
        DespawnObject();
    }

    protected abstract bool CanDestroy();
}
