using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskHelper : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    public static bool ObjectInLayerMask(GameObject gameObject, LayerMask layerMask)
    {
        if ((layerMask.value & (1 << gameObject.layer)) > 0)
        {
            return true;
        }
        return false;
    }
    public static LayerMask CreatLayerMask(params int[] layers)
    {
        LayerMask layerMask = 0;
        foreach (int layer in layers)
        {
            layerMask |= (1 << layer);
        }
        return layerMask;
    }
    public static LayerMask GetLayerMask(GameObject gameObject)
    {
        return 1 << gameObject.layer;
    }
}
