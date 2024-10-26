using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByHit : Despawn
{
    
    protected override bool CanDestroy()
    {
        throw new System.NotImplementedException();
    }
    protected override void DespawnObject()
    {
        SpawnEnemy.Instance.Despawn(transform.parent);
    }
}
