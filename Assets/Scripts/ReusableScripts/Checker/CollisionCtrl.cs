using UnityEditor;
using UnityEngine;

public class CollisionCtrl : BaseMovement
{
    public bool OnGround { get => checkedGround;  private set { } }
    public bool OnWallRight { get => checkedWallRight;  private set { } }
    public bool OnWallLeft { get => checkedWallLeft; private set { } }

    [SerializeField] Transform _wallRightPoint;
    [SerializeField] Transform _wallLeftPoint;
    [SerializeField] Transform _groundPoint;

    [SerializeField] private bool checkedGround;
    [SerializeField] private bool checkedWallRight;
    [SerializeField] private bool checkedWallLeft;

    [SerializeField] private Color groundGizmoColor = Color.magenta;
    [SerializeField] private Color wallLeftGizmoColor = Color.yellow;
    [SerializeField] private Color wallRightGizmoColor = Color.red;

    private void Start()
    {
        _rb.gravityScale = _stat.DefaultGravityScale;
    }
    private void Awake()
    {
        LoadComponents();
    }

    private void FixedUpdate()
    {
        CheckCollision();
        //CheckCircle();  
        SetGravity();
        //CheckCollisionWithRaycast();
    }
    protected override void LoadComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _stat = Resources.Load<PlayerStatsSO>("PlayerStats");
    }
    #region Gravity
    private void SetGravity()
    {
        if (WallActionCtrl._isWallSliding) _rb.gravityScale = 1;
        else if (WallActionCtrl._isWallClimbing) _rb.gravityScale = 0;
        else if ((JumpCtrl._isJumping || JumpCtrl._isFalling))
        {
            _rb.gravityScale = _stat.JumpFallGravity;
        }
        else
        {
            _rb.gravityScale = _stat.DefaultGravityScale;
        }
    }
    #endregion
    #region Box Check
    private void CheckCollision()
    {

        //Vector2 colliderBottomPoint = new Vector2(_colFeet.bounds.center.x, _colFeet.bounds.min.y);
        checkedGround = Physics2D.OverlapBox(_groundPoint.position, _stat.GroundCheckSize, 0f, _stat.GroundLayer) != null;

        //Vector2 colliderLeftPoint = new Vector2(_colFeet.bounds.min.x, _colFeet.bounds.center.y);
        checkedWallLeft = Physics2D.OverlapBox(_wallLeftPoint.position, _stat.WallCheckSize, 0f, _stat.GroundLayer) != null;

        //Vector2 colliderRightPoint = new Vector2(_colFeet.bounds.max.x, _colFeet.bounds.center.y);
        checkedWallRight = Physics2D.OverlapBox(_wallRightPoint.position, _stat.WallCheckSize, 0f, _stat.GroundLayer) != null;
    }
    private void OnDrawGizmos()
    {
        if (_rb == null) return;
        Gizmos.color = groundGizmoColor;
        Gizmos.DrawWireCube(_groundPoint.position, _stat.GroundCheckSize);

        Gizmos.color = wallLeftGizmoColor;
        Gizmos.DrawWireCube(_wallLeftPoint.position, _stat.WallCheckSize);

        Gizmos.color = wallRightGizmoColor;
        Gizmos.DrawWireCube(_wallRightPoint.position, _stat.WallCheckSize);
    }

    #endregion
    #region Circle Check
    /*public void CheckCircle()
    {
        float checkRadius = _stat.grounDistance;  

        Vector2 groundCheckPoint = new Vector2(_colFeet.bounds.center.x, _colFeet.bounds.min.y);
        checkedGround = Physics2D.OverlapCircle(groundCheckPoint, checkRadius, _stat.GroundLayer) != null;

        Vector2 wallLeftCheckPoint = new Vector2(_colFeet.bounds.min.x, _colFeet.bounds.center.y);
        checkedWallLeft = Physics2D.OverlapCircle(wallLeftCheckPoint, checkRadius, _stat.GroundLayer) != null;

        Vector2 wallRightCheckPoint = new Vector2(_colFeet.bounds.max.x, _colFeet.bounds.center.y);
        checkedWallRight = Physics2D.OverlapCircle(wallRightCheckPoint, checkRadius, _stat.GroundLayer) != null;
    }
    private void OnDrawGizmos()
    {
        if (_colFeet == null) return;

        Gizmos.color = groundGizmoColor;
        float checkRadius = _stat.grounDistance;

        Vector2 groundCheckPoint = new Vector2(_colFeet.bounds.center.x, _colFeet.bounds.min.y);
        Gizmos.DrawWireSphere(groundCheckPoint, checkRadius);

        Gizmos.color = wallLeftGizmoColor;
        Vector2 wallLeftCheckPoint = new Vector2(_colFeet.bounds.min.x, _colFeet.bounds.center.y);
        Gizmos.DrawWireSphere(wallLeftCheckPoint, checkRadius);

        Gizmos.color = wallRightGizmoColor;
        Vector2 wallRightCheckPoint = new Vector2(_colFeet.bounds.max.x, _colFeet.bounds.center.y);
        Gizmos.DrawWireSphere(wallRightCheckPoint, checkRadius);
    }*/
    #endregion
    #region Raycast Check with contactFilter
    /*[SerializeField] private ContactFilter2D contactFilter;
    CapsuleCollider2D capsuleCollider2;
    RaycastHit2D[] hitResults = new RaycastHit2D[5];
    void CheckCollisionWithRaycast()
    {
        float rayLength = _stat.grounDistance;

        contactFilter.layerMask = _stat.GroundLayer;
        contactFilter.useLayerMask = true;
        

        Vector2 groundCheckStartPoint = new Vector2(_colFeet.bounds.center.x, _colFeet.bounds.min.y);
        checkedGround = capsuleCollider2.Cast(Vector2.down, contactFilter, hitResults, rayLength) > 0;

        Vector2 wallLeftCheckPoint = new Vector2(_colFeet.bounds.min.x, _colFeet.bounds.center.y);
        checkedWallLeft = capsuleCollider2.Cast(Vector2.left, contactFilter, hitResults, rayLength) > 0;

        Vector2 wallRightCheckPoint = new Vector2(_colFeet.bounds.max.x, _colFeet.bounds.center.y);
        checkedWallRight = capsuleCollider2.Cast(Vector2.right, contactFilter, hitResults, rayLength) > 0;

    }

    private void OnDrawGizmos()
    {
        if (_colFeet == null) return;

        float rayLength = _stat.grounDistance;

        Gizmos.color = groundGizmoColor;
        Vector2 groundCheckStartPoint = new Vector2(_colFeet.bounds.center.x, _colFeet.bounds.min.y);
        Gizmos.DrawLine(groundCheckStartPoint, groundCheckStartPoint + Vector2.down * rayLength);

        Gizmos.color = wallLeftGizmoColor;
        Vector2 wallLeftCheckPoint = new Vector2(_colFeet.bounds.min.x, _colFeet.bounds.center.y);
        Gizmos.DrawLine(wallLeftCheckPoint, wallLeftCheckPoint + Vector2.left * rayLength);

        Gizmos.color = wallRightGizmoColor;
        Vector2 wallRightCheckPoint = new Vector2(_colFeet.bounds.max.x, _colFeet.bounds.center.y);
        Gizmos.DrawLine(wallRightCheckPoint, wallRightCheckPoint + Vector2.right * rayLength);
    }*/


    #endregion

    #region Collision Particle
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Particle hit the player!");
        
            HandlePlayerHit();
        }
    }

    private void HandlePlayerHit()
    {
        Debug.Log("Player was hit by particle!");
        
    }

    #endregion
}
