using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public Rigidbody2D rb;
    public float HorizontalMove = 0f;

    [Header("Player move settings")]
    [Range(0, 10f)] public float speed = 1f;
    [Range(0, 15f)] public float jumpforce = 5f;

    [Space]
    [Header("Ground Checking settings")]
    public bool isOnGround = false;
    [Range(-5f, 5f)]public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)]public float checkGroundRadius = 0.3f;



    private bool facingrotate = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (isOnGround && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
        }

        if (HorizontalMove < 0 && facingrotate)
        {
            flip();
        }
        else if (HorizontalMove > 0 && !facingrotate)
        {
            flip();
        }
    }

    public Vector2 moveVector;
    void FixedUpdate()
    {
        moveVector = new Vector2(HorizontalMove * 2f, rb.velocity.y);
        rb.velocity = moveVector;
        CheckGround();
    }

    void flip()
    {
        facingrotate = !facingrotate;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);

        if (colliders.Length > 1)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
}
