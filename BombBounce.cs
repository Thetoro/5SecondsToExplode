using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBounce : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        _currentVelocity = _rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var speed = _currentVelocity.magnitude;
        var direction = Vector2.Reflect(_currentVelocity.normalized, other.contacts[0].normal);

        _rb.velocity = direction * speed;
    }
}
