using UnityEngine;

[CreateAssetMenu]
public class UseableStats : ScriptableObject
{
    [Header("LAYERS")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;

    [Header("Walk")]
    public float WalkSpeed = 14f;
    public float WalkGroundAcceleration = 120f;
    public float WalkAirAcceleration = 60f;

    [Header("Jump")]
    public float JumpHeight = 5f;
    public int JumpCount = 0;
    public float JumpCutOff = 0.5f;

    public float CoyoteTime = 0.15f;
    public float BufferJump = 0.2f;

    [Header("Wall")]
    public float WallSlideSpeed = 5f;
    public float WallJumpForce = 15f;
    public float WallHoldTime = 10f;

    [Header("Dash")]
    public float DashSpeed = 1f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;
}
