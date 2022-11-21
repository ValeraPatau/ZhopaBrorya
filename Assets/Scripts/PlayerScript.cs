using System.Collections;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float HorizontalMove = 0f;

    [Header("Player move settings")]
    [Range(0, 10f)] public float speed = 1f;
    [Range(0, 1080f)] public float jumpforce = 5f;
    


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

    
    void Jump()
    {
        if (isOnGround && Input.GetKeyDown(KeyCode.W) && isOnGround )
        {
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }
        if  (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }

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
