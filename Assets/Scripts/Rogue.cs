using UnityEngine;

public class Rogue : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Rigidbody2D _body;
    private SpriteRenderer _spriteRenderer;
    private float _moveDirection;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveDirection = 1;
    }

    private void Update()
    {
        float xVelocity = speed * _moveDirection;
        _body.velocity = new Vector2(xVelocity, _body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag(Constants.WALL_TAG))
        {
            ChangeMoveDirection();
        }
    }

    private void ChangeMoveDirection()
    {
        _moveDirection *= -1;
        _spriteRenderer.flipX = _moveDirection < 0;
    }
}