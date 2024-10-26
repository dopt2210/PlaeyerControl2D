using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : Spawner
{
    private static SpawnEnemy instance;
    public static SpawnEnemy Instance { get => instance;  }
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Not Allow 2 Spawner");
        instance = this;
    }

}
