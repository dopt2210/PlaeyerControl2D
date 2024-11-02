using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindComponentHelper : MonoBehaviour
{
    public static T LoadComponentObject<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component != null) return component;
        component = go.GetComponentInParent<T>();
        if (component != null) return component;
        component = go.GetComponentInChildren<T>();
        if (component != null) return component;
        return null;
    }
}
