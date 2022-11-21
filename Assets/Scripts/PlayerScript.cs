using System.Collections;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float HorizontalMove = 0f;

    [Header("Player move settings")]
    [Range(0, 10f)] public float speed = 1f;
    
    


    [Space]
    [Header("Ground Checking settings")]
    public bool isOnGround = false;
    [Range(-5f, 5f)]public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)]public float checkGroundRadius = 0.3f;
    public Vector2 moveVector;
    private bool facingrotate = true;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Walk();
        flip();
        
        CheckGround();
    }

    void FixedUpdate()
    {
        Jump();
    }

    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    void flip()
    {
        if ((moveVector.x > 0 && !facingrotate) || (moveVector.x < 0 && facingrotate))
        {
            transform.localScale *= new Vector2(-1, 1);
            facingrotate = !facingrotate;
        }
    }

    
    public float jumpForce = 210f;
    private bool jumpControl;
    private float jumpIteration = 0;
    public float jumpValueIteration = 60;
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isOnGround) { jumpControl = true; }

        }      
        else { jumpControl = false; }
        if (jumpControl)
        {
            if (jumpIteration++ < jumpValueIteration)
            {
            rb.AddForce(Vector2.up * jumpForce / jumpIteration);
            }
        }
        else { jumpIteration = 0; }
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
