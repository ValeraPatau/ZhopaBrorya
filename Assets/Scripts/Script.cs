using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//назови нормально скрипт
public sealed class Script : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float HorizontalMove = 0f;

    [Header("Player move settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpforce = 5f;

    [Space]
    [Header("Ground Checking settings")]
    private bool isOnGround = false;
    [SerializeField] private float checkGroundOffsetY = -1.8f;
    [Range(0, 5f), SerializeField] private float checkGroundRadius = 0.3f;
    
    private bool facingrotate = true;
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnValidate(){
        speed = Mathf.Clamp(speed, 0, float.MaxValue);
        jumpforce = Mathf.Clamp(jumpforce, 0, float.MaxValue);
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
        //откуда 2
        moveVector = new Vector2(HorizontalMove * 2f, rb.velocity.y);
        rb.velocity = moveVector;
        //очень дорого рекомендую переделать при проверки КОЛЛИЗИИ
        CheckGround();
    }

    void flip()
    {
        facingrotate = !facingrotate;
        Vector3 scale = transform.localScale;
        //не рекомендую так делать потому что все дочерние объекты такой же скейл получат
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
    
    public bool IsOnGround => isOnGround;
}
