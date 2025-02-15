using UnityEngine;

public class MoveCtrl : BaseMovement, IMoveAble
{
    private DirectionCtrl mDirectionCtrl;
    [SerializeField] private float _acceleration, _speedModifier, _maxSpeed, _speedChange;

    public bool _isFacingRight { get; set; }
    public bool _isCanMove { get; set; }

    private void Awake()
    {
        LoadComponents();
        mDirectionCtrl = transform.parent.GetComponentInChildren<DirectionCtrl>();
        _isFacingRight = true;
    }

    private void FixedUpdate()
    {
        SetFacingDirection(PlayerCtrl.instance.Move);
        HandleMove(1);
        
        _anim.SetFloat("Move", Mathf.Abs(_maxSpeed));
        _anim.SetBool("OnGround", _collisionCtrl.OnGround);
    }
    #region Others

    #endregion
    #region Move

    public void HandleMove(float learpAmount)
    {
        _maxSpeed = PlayerCtrl.instance.MoveX * _stat.WalkSpeed;

        _maxSpeed = Mathf.Lerp(_rb.velocity.x, _maxSpeed, learpAmount);

        _speedChange = _maxSpeed - _rb.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) && _collisionCtrl.OnGround ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = _speedChange * _acceleration;

        //_speedModifier = Mathf.Pow(Mathf.Abs(_acceleration * _speedChange), _stat.AccelerationPower) * Time.fixedDeltaTime;

        //_velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier / _rb.mass);
        //if(Mathf.Abs(_rb.velocity.x) < 1e-5f || Mathf.Abs(_rb.velocity.y) < 1e-5f) {_rb.velocity = Vector2.zero; }
        _rb.AddForce(_speedModifier * Vector2.right, ForceMode2D.Force);
        //if (Mathf.Abs(_rb.velocity.x) < 1e-5f || Mathf.Abs(_rb.velocity.y) < 1e-5f) _rb.velocity = Vector2.zero;
    }

    public void SetFacingDirection(Vector2 direction)
    {
        if (direction.x > 0 && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 rotation = new Vector3(transform.parent.rotation.x, 0f, transform.parent.rotation.z);
            transform.parent.rotation = Quaternion.Euler(rotation);

            mDirectionCtrl.CallTurn();
        }
        else if (direction.x < 0 && _isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 rotation = new Vector3(transform.parent.rotation.x, 180f, transform.parent.rotation.z);
            transform.parent.rotation = Quaternion.Euler(rotation);

            mDirectionCtrl.CallTurn();
        }
    }
    #endregion
}
