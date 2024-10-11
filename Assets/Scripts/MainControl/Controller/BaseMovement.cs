using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [HideInInspector] protected static UseableStats _stat;
    [HideInInspector] protected static CollisionCtrl _collisionCtrl;
    [HideInInspector] protected static Rigidbody2D _rb;
    [HideInInspector] protected static Animator _anim;

    protected virtual void Awake()
    {
        _stat = AssetDatabase.LoadAssetAtPath<UseableStats>("Assets/ScriptableObject/_stats.asset");
        _rb = transform.parent.GetComponent<Rigidbody2D>();
        _anim = transform.parent.GetComponent<Animator>();
        _collisionCtrl = transform.parent.GetComponentInChildren<CollisionCtrl>();
    }
    public static void LogCaller(
      [System.Runtime.CompilerServices.CallerLineNumber] int line = 0
    , [System.Runtime.CompilerServices.CallerMemberName] string memberName = ""
    , [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
)
    {
        // Can replace UnityEngine.Debug.Log with any logging API you want
        UnityEngine.Debug.Log($"{line} :: {memberName} :: {filePath}");
    }

    protected static void ApplyMovement(Vector2 _velocity) =>  _rb.velocity = _velocity; 
}
