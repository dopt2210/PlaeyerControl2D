using UnityEditor;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [HideInInspector] protected static UseableStats _stat;
    [HideInInspector] protected static Rigidbody2D _rb;
    [HideInInspector] protected static Animator _anim;
    [HideInInspector] protected static TrailRenderer _tr;
    [HideInInspector] protected static CollisionCtrl _collisionCtrl;

    protected virtual void Awake()
    {

    }
    protected virtual void Reset()
    {

    }

    protected virtual void LoadComponents()
    {
        _stat = Resources.Load<UseableStats>("_stats");
        _rb = GetComponentInParent<Rigidbody2D>();
        _anim = GetComponentInParent<Animator>();
        _tr = GetComponentInParent<TrailRenderer>();
        _collisionCtrl = GetComponentInParent<CollisionCtrl>();
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

}
