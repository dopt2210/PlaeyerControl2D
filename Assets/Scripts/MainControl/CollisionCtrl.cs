using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCtrl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    Collider2D _col;
    PlayerCtrl _playerCtrl;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        _playerCtrl = GetComponent<PlayerCtrl>();
    }

    public bool isGrounded;
    public bool isWallRight;
    public bool isWallLeft;
    public bool isCeiling;

    private Vector2 boxSize;
    private float grounDistance = 0.1f;
    private float boxOffsetX = 0.5f;

    public void CheckCollision()
    {
        boxSize = new Vector2(_col.bounds.size.x, grounDistance);
        //Vector2 colliderBottom = new Vector2(tranform.position.x, transform.position.y-boxOffsetY);
        Vector2 colliderBottom = new Vector2(_col.bounds.center.x, _col.bounds.min.y);
        isGrounded = Physics2D.OverlapBox(colliderBottom, boxSize, 0f, _stat.GroundLayer) != null;
        //isGrounded = Physics2D.Raycast(colliderBottom, Vector2.down, groundCheckDistance, GroundLayer);

        Vector2 colliderTop = new Vector2(_col.bounds.center.x, _col.bounds.max.y);
        isCeiling = Physics2D.OverlapBox(colliderTop, boxSize, 0f, _stat.GroundLayer) != null;

        Vector2 colliderLeft = new Vector2(transform.position.x - boxOffsetX, transform.position.y);
        isWallLeft = Physics2D.OverlapBox(colliderLeft, boxSize, 0f, _stat.GroundLayer) != null;

        Vector2 colliderRight = new Vector2(transform.position.x + boxOffsetX, transform.position.y);
        isWallRight = Physics2D.OverlapBox(colliderRight, boxSize, 0f, _stat.GroundLayer) != null;
    }
}
