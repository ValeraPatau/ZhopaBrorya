using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    
    private Rigidbody2D _charRigidbody;
    private Collider2D _charCollider;

    private float _moveDirection;
    
    void Start()
    {
        _charRigidbody = GetComponent<Rigidbody2D>();
        _charCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        charMove();
        charJump();
    }
    
    void charMove()
    {
        _charRigidbody.velocity = new Vector2(_moveDirection * moveSpeed, _charRigidbody.velocity.y);
        _moveDirection = Input.GetAxisRaw("Horizontal");
    }

    void charJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _charRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
