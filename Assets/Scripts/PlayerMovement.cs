using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpPower = 6f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _body;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _isGrounded;
    private bool _jumped;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckIfGround();
        Jump();
    }

    private void FixedUpdate()
    {
        HorizontalMove();
    }

    private void HorizontalMove()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * _speed;

        switch (horizontalMove)
        {
            case > 0:
                ChangeMoveDirection(1);
                break;

            case < 0:
                ChangeMoveDirection(-1);
                break;
        }

        Vector2 velocity = _body.velocity;
        velocity = new Vector2(horizontalMove, velocity.y);
        _body.velocity = velocity;

        _animator.SetFloat(Constants.PLAYER_SPEED, Mathf.Abs(velocity.x));
    }

    private void ChangeMoveDirection(float direction)
    {
        _spriteRenderer.flipX = direction < 0;
    }

    private void CheckIfGround()
    {
        _isGrounded = Physics2D.Raycast(_groundCheck.position, Vector2.down, Constants.RAYCAST_DISTANCE, _groundLayer);
        if (_isGrounded == false)
            return;

        if (_jumped == false)
            return;

        _jumped = false;
        _animator.SetBool(Constants.PLAYER_JUMP, false);
    }

    private void Jump()
    {
        if (_isGrounded == false)
            return;

        if (Input.GetKey(KeyCode.Space) == false)
            return;

        _jumped = true;
        _body.velocity = new Vector2(_body.velocity.x, _jumpPower);
        _animator.SetBool(Constants.PLAYER_JUMP, true);
    }
}