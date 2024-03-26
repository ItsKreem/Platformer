using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float JumpForce = 50f;

    //ground check
    public Transform GroundCheck;
    public float GroundCheckRadius = 1f;
    public float MaxSlopeAngle = 45f;

    public Cooldown CoyoteTime;
    public Cooldown BufferJump;

    public LayerMask GroundLayerMask;

    protected bool _isGrounded = false;
    protected bool _isJumping = false;
    protected bool _isRunning = false;
    protected bool _isFalling = false;
    protected bool _canJump = true;
    public bool FlipAnim;
    public bool IsFalling
    {
        get { return _isFalling; }
    }

    public bool IsJumping
    {
        get { return _isJumping; }
    }

    public bool IsGrounded
    {
        get { return _isGrounded; }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
    }

    protected Vector2 _inputDirection;

    protected Rigidbody2D _rigidbody2D;
    protected Collider2D _collider2D;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        CheckGround();

        HandleMovement();

        Flip();
    }

    protected virtual void HandleInput()
    {
 
    }

    protected void TryBufferJump()
    {
        BufferJump.StartCooldown();
    }

    protected virtual void DoJump()
    {
        TryBufferJump();

        if (!_canJump)
            return;

        if (CoyoteTime.CurrentProgress == Cooldown.Progress.Finished)
            return;

        _canJump = false;
        _isJumping = true;

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);

        CoyoteTime.StopCooldown();
    }

    protected virtual void HandleMovement()
    {
        Vector3 targetVelocity = Vector3.zero;

        if (_isGrounded && !_isJumping)
        {
            targetVelocity = new Vector2(_inputDirection.x * (Acceleration), 0f);
        }
        else
        {
            targetVelocity = new Vector2(_inputDirection.x * (Acceleration), _rigidbody2D.velocity.y);
        }

        _rigidbody2D.velocity = targetVelocity;

        if (targetVelocity.x == 0)
        {
            _isRunning = false;
        }
        else
        {
            _isRunning = true;
        }
    }

    protected virtual void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayerMask);

        if (_rigidbody2D.velocity.y <= 0)
        {
            _isJumping = false;
        }

        if (_isGrounded && !IsJumping)
        {
            _canJump = true;

            if (CoyoteTime.CurrentProgress != Cooldown.Progress.Ready)
                CoyoteTime.StopCooldown();

            if (_isJumping)
                DoJump();
        }

        if (BufferJump.CurrentProgress is Cooldown.Progress.Started or Cooldown.Progress.InProgress)
        {
            DoJump();
        }

        if (!_isGrounded && !IsJumping && CoyoteTime.CurrentProgress == Cooldown.Progress.Ready)
            CoyoteTime.StartCooldown();
    }

    protected virtual void Flip()
    {
        if (_inputDirection.x == 0)
            return;

        if(_inputDirection.x > 0)
        {
            FlipAnim = false;
        }
        else if (_inputDirection.x < 0)
        {
            FlipAnim = true;
        }
    }
}
