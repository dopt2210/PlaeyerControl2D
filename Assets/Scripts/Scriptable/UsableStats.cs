using UnityEngine;

[CreateAssetMenu]
public class UseableStats : ScriptableObject
{
    [Header("LAYERS")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;

    [Header("Walk")]
    public float WalkSpeed = 11f;

    [Header("Acceleration")]
    public float Acceleration = 16f;
    public float Deceleration = 13f;
    public float AccelerationPower = 1.2f;

    [Header("Jump")]
    public float JumpHeight = 4f;
    public int JumpCount = 0;
    public float JumpCutOffMultipiler = 0.5f;
    public float JumpApexTime = 1.2f;
    public float JumpForce;

    public float JumpCoyoteTime = 0.15f;
    public float JumpBufferTime = 0.2f;

    [Header("Gravity")]
    [HideInInspector] public float DefaultGravityScale;
    [HideInInspector] public float GravityStrength;
    public float JumpFallGravity = 1.9f;

    [Header("Wall")]
    public float WallClimbSpeed = 5f;
    public float WallSlideSpeed = 20f;
    public float WallJumpForce = 15f;
    public float WallHoldTime = 2f;
    public float WallLerp = 0.5f;

    [Header("Dash")]
    public float DashSpeed = 1f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;

    [Header("Collision")]
    public Vector2 GroundCheckSize = new Vector2(0.9f, 0.12f);
    public Vector2 WallCheckSize = new Vector2(0.2f, 1.2f);

    private void OnValidate()
    {
        GravityStrength = -(2 * JumpHeight) / (JumpApexTime * JumpApexTime);

        DefaultGravityScale = GravityStrength / Physics2D.gravity.y;

        JumpForce = Mathf.Abs(GravityStrength) * JumpApexTime;

        //Acceleration = Mathf.Clamp(Acceleration, 0.01f, WalkSpeed);
        //Deceleration = Mathf.Clamp(Deceleration, 0.01f, WalkSpeed);
    }
}
