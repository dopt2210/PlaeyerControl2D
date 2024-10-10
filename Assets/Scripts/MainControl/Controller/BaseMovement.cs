using System.Collections;
using System.Collections.Generic;
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
}
