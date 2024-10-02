using UnityEngine;

public class CollisionCtrl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    private Collider2D _col;
    private bool checkedGround;
    private bool checkedWallRight;
    private bool checkedWallLeft;
    private bool checkedCeiling;

    private Vector2 boxSize;
    private float grounDistance = 0.1f;
    private float boxOffsetX = 0.5f;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    public void CheckCollision()
    {
        boxSize = new Vector2(_col.bounds.size.x, grounDistance);
        //Vector2 colliderBottom = new Vector2(tranform.position.x, transform.position.y-boxOffsetY);
        Vector2 colliderBottom = new Vector2(_col.bounds.center.x, _col.bounds.min.y);
        checkedGround = Physics2D.OverlapBox(colliderBottom, boxSize, 0f, _stat.GroundLayer) != null;
        //OnGround = Physics2D.Raycast(colliderBottom, Vector2.down, groundCheckDistance, GroundLayer);

        Vector2 colliderTop = new Vector2(_col.bounds.center.x, _col.bounds.max.y);
        checkedCeiling = Physics2D.OverlapBox(colliderTop, boxSize, 0f, _stat.GroundLayer) != null;

        Vector2 colliderLeft = new Vector2(transform.position.x - boxOffsetX, transform.position.y);
        checkedWallLeft = Physics2D.OverlapBox(colliderLeft, boxSize, 0f, _stat.GroundLayer) != null;

        Vector2 colliderRight = new Vector2(transform.position.x + boxOffsetX, transform.position.y);
        checkedWallRight = Physics2D.OverlapBox(colliderRight, boxSize, 0f, _stat.GroundLayer) != null;
    }

    public bool OnGround() { return checkedGround; }
    public bool OnWallRight() { return checkedWallRight; }
    public bool OnWallLeft() { return checkedWallLeft; }
    public bool HitCeiling() { return checkedCeiling; }
}
