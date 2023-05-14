using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    private SpriteRenderer sprite;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // 추후 여기 수정해서 스페이스 누르는 시간에 따라 점프하도록
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
}
