using UnityEngine;

[CreateAssetMenu]
public class UseableStats : ScriptableObject
{
    [Header("LAYERS")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;

    [Header("Walk")]
    public float WalkSpeed = 9f;

    [Header("Acceleration")]
    public float Acceleration = 16f;
    public float Deceleration = 13f;
    public float AccelerationPower = 1.2f;
    public float DefaultGravityScale = 1f;

    [Header("Jump")]
    public float JumpHeight = 12f;
    public int JumpCount = 0;
    public float JumpCutOffMultipiler = 0.1f;
    
    public float JumpCoyoteTime = 0.15f;
    public float JumpBufferTime = 0.2f;

    public float JumpFallGravity = 1.9f;

    [Header("Wall")]
    public float WallClimbSpeed = 5f;
    public float WallSlideSpeed = 20f;
    public float WallJumpForce = 15f;
    public float WallHoldTime = 2f;

    [Header("Dash")]
    public float DashSpeed = 1f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;

    [Header("Collision")]
    public float grounDistance = 0.1f;
}
