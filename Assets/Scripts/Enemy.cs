using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 3f;

    private SpriteRenderer _spriteRenderer;
    private int _currentWaypointIndex;
    private int _direction = 1;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Transform wp = _points[_currentWaypointIndex];

        if (Vector3.Distance(transform.position, wp.position) < 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _points.Length;
            ChangeDirection();
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                _speed * Time.deltaTime);
        }
    }

    private void ChangeDirection()
    {
        _direction *= -1;
        _spriteRenderer.flipX = _direction < 0;
    }
}