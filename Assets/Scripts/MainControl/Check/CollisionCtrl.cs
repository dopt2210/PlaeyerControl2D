using UnityEngine;

public class CollisionCtrl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    [SerializeField] Animator _anim;

    private Collider2D _col;
    private bool checkedGround;
    private bool checkedWallRight;
    private bool checkedWallLeft;
    private bool checkedCeiling;

    private Vector2 boxSizeX;
    private Vector2 boxSizeY;
    private int hitNumber = 0;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        boxSizeX = new Vector2(_col.bounds.size.x,_stat.grounDistance);
        boxSizeY = new Vector2(_stat.grounDistance, _col.bounds.size.y);

        Vector2 colliderBottom = new Vector2(_col.bounds.center.x, _col.bounds.min.y);
        checkedGround = Physics2D.OverlapBox(colliderBottom, boxSizeX, 0f, _stat.GroundLayer) != null;
        
        Vector2 colliderTop = new Vector2(_col.bounds.center.x, _col.bounds.max.y);
        checkedCeiling = Physics2D.OverlapBox(colliderTop, boxSizeX, 0f, _stat.GroundLayer) != null;

        Vector2 colliderLeft = new Vector2(_col.bounds.min.x, _col.bounds.center.y);
        checkedWallLeft = Physics2D.OverlapBox(colliderLeft, boxSizeY, 0f, _stat.GroundLayer) != null;

        Vector2 colliderRight = new Vector2(_col.bounds.max.x, _col.bounds.center.y);
        checkedWallRight = Physics2D.OverlapBox(colliderRight, boxSizeY, 0f, _stat.GroundLayer) != null;
    }

    [SerializeField] private Color groundGizmoColor = Color.green;
    [SerializeField] private Color ceilingGizmoColor = Color.blue;
    [SerializeField] private Color wallLeftGizmoColor = Color.yellow;
    [SerializeField] private Color wallRightGizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        if (_col == null) return; 

        Gizmos.color = groundGizmoColor;
        boxSizeX = new Vector2(_col.bounds.size.x, _stat.grounDistance);
        boxSizeY = new Vector2(_stat.grounDistance, _col.bounds.size.y);

        Vector2 colliderBottom = new Vector2(_col.bounds.center.x, _col.bounds.min.y);
        Gizmos.DrawWireCube(colliderBottom, boxSizeX);

        Gizmos.color = ceilingGizmoColor;
        Vector2 colliderTop = new Vector2(_col.bounds.center.x, _col.bounds.max.y);
        Gizmos.DrawWireCube(colliderTop, boxSizeX);

        Gizmos.color = wallLeftGizmoColor;
        Vector2 colliderLeft = new Vector2(_col.bounds.max.x, _col.bounds.center.y);
        Gizmos.DrawWireCube(colliderLeft, boxSizeY);

        Gizmos.color = wallRightGizmoColor;
        Vector2 colliderRight = new Vector2(_col.bounds.min.x, _col.bounds.center.y);
        Gizmos.DrawWireCube(colliderRight, boxSizeY);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            Debug.LogWarning("Hit");
            hitNumber++;
            if (hitNumber == 2)
            {
                _anim.SetBool("Dead", true);
                hitNumber = 0;

            }
            else _anim.SetBool("Dead", false);
            _anim.SetBool("HitTrap", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            _anim.SetBool("HitTrap", false);
        }
    }

    public bool OnGround() { return checkedGround; }
    public bool OnWallRight() { return checkedWallRight; }
    public bool OnWallLeft() { return checkedWallLeft; }
    public bool HitCeiling() { return checkedCeiling; }
}
