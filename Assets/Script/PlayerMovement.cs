using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask jumbleGround;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    public bool gameIsActive = false;
    private float startHeight = 2;
    private float rightWidth = 10;
    private float leftWidth = -10;
    public float speed = 1f;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // 추후 여기 수정해서 스페이스 누르는 시간에 따라 점프하도록
        }
        
        Debug.Log(transform.position.x);

        if(transform.position.x < leftWidth){
            transform.position = transform.position + (Vector3.right * 20);
        }else if (transform.position.x > rightWidth){
            transform.position = transform.position + (Vector3.left * 20);
        }

        if(transform.position.y > startHeight)
        {
            gameIsActive = true;
        }

        if(gameIsActive && IsGrounded())
        {
            transform.position = transform.position + (Vector3.down * speed) * Time.deltaTime;
        }

        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate() 
    {
        MovementState state;

        if (dirX > 0f) {
            state = MovementState.running;
            sprite.flipX = false;
        } else if (dirX < 0f) {
            state = MovementState.running;
            sprite.flipX = true;
        } else {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } else if (rb.velocity.y < -.1f) {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumbleGround);
    }
}
