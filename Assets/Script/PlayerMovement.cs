using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    // [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask jumbleGround;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    public bool gameIsActive = false;
    private float startHeight = 2;
    // private float rightWidth = 10;
    // private float leftWidth = -10;
    public float speed = 1f;
    private float _maxTime=1f;  // 최대로 눌리는 시간
    private float _pressTime;   // 눌렀을때 시간 
    public GameObject jumpgauge;
    private bool _canJump = true;   // 점프 가능 여부
    private bool _canMove = true;  // 좌우 변경 가능 여부

    public bool playerIsActive = true;
    private float deadLine = -5;
    public Logic logic;

    private enum MovementState { idle, running, jumping, falling, ready }
    private MovementState _currentState;

    public float wallBounceForce = 3f;
    public LayerMask groundLayer;
    public TilemapCollider2D tilemapCollider;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        _currentState = MovementState.idle;
        jumpgauge.SetActive(false);

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
    }

    private void DriveJump()
    {
        if (_currentState == MovementState.ready)
        {
            _pressTime += Time.deltaTime;
            // Debug.Log(_pressTime);
        }
    }

    private void CanMove()
    {
        if (_currentState == MovementState.ready)
        {
            moveSpeed = 0;
        } 
        else if (_currentState == MovementState.jumping || _currentState == MovementState.falling)
        {
            _canJump = false;
            _canMove = false;
        }
        else
        {
            _canJump = true;
            _canMove = true;
            moveSpeed = 5f;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyUp(KeyCode.Space) && IsGrounded() && playerIsActive) {
            // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // 추후 여기 수정해서 스페이스 누르는 시간에 따라 점프하도록

            jumpgauge.SetActive(false);
            _pressTime = Mathf.Clamp(_pressTime, 0f, _maxTime); // 최소 0초에서 최대 1초 동안 점프 기준을 정함
        
            float y = Mathf.Lerp(4f, 8f, _pressTime)/1.5f;
            float x = Mathf.Lerp(2f, 6f, _pressTime)/1.5f;
            
            // 점프 이벤트
            if (sprite.flipX) // 왼쪽 보고 있을 때 
            {
                
                // rb.velocity = new Vector2(-x, y);
                JumpForce(new Vector2(-x, y));
                // Debug.Log("왼쪽" + rb.velocity);
            }
            else // 오른쪽 보고 있을떄
            {
                // rb.velocity = new Vector2(x, y);
                JumpForce(new Vector2(x, y));
                // Debug.Log("오른쪽" + rb.velocity);
            }

            _pressTime = 0f;
        }
    }
    private void JumpForce(Vector2 maxHeightDisplacement)
    {
        rb.gravityScale = 8; // 중력 조절, 이걸로 점프 속도 조절 가능함
        // Debug.Log(rb.gravityScale);
        // m*k*g*h = m*v^2/2 (단, k == gravityScale) <= 역학적 에너지 보존 법칙 적용
        float v_y = Mathf.Sqrt(2 * rb.gravityScale * -Physics2D.gravity.y * maxHeightDisplacement.y);
        // 포물선 운동 법칙 적용
        float v_x = maxHeightDisplacement.x * v_y / (2 * maxHeightDisplacement.y);

        Vector2 force = rb.mass * (new Vector2(v_x, v_y) - rb.velocity);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        DriveJump();
        Jump();
        CanMove();

        if (!isJumping && IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (playerIsActive && _canMove)
        {
            dirX = Input.GetAxis("Horizontal");
            // rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            transform.position += Vector3.right * (dirX * Time.deltaTime * moveSpeed);
        }
        

        // if(transform.position.x < leftWidth){
        //     transform.position = transform.position + (Vector3.right * 20);
        // }else if (transform.position.x > rightWidth){
        //     transform.position = transform.position + (Vector3.left * 20);
        // }

        if(transform.position.y > startHeight)
        {
            gameIsActive = true;
        }

        if(transform.position.y < deadLine && playerIsActive){
            playerIsActive = false;
            gameIsActive = false;
            logic.gameOver();
        }

        if(gameIsActive && IsGrounded())
        {
            transform.position = transform.position + (Vector3.down * speed) * Time.deltaTime;
        }

        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate() 
    {
        // MovementState state;

        if (IsGrounded())
        {
            if (dirX > 0f)
            {
                _currentState = MovementState.running;
                sprite.flipX = false;
            }
            else if (dirX < 0f)
            {
                _currentState = MovementState.running;
                sprite.flipX = true;
            }
            else
            {
                _currentState = MovementState.idle;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_canJump && IsGrounded()) 
            {
                // Debug.Log("Space");
                _currentState = MovementState.ready;
                jumpgauge.SetActive(true);
            }
        }

        if (rb.velocity.y > .1f)
        {
            _currentState = MovementState.jumping;
        } else if (rb.velocity.y < -.1f) {
            _currentState = MovementState.falling;
        }

        // Debug.Log(_currentState);
        anim.SetInteger("state", (int)_currentState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumbleGround);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            ApplyGravity();
        }
    }
    private void ApplyGravity()
    {
        rb.gravityScale = 1f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Tilemap"))
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 bounceDirection = Vector2.Reflect(rb.velocity.normalized, normal);
            rb.velocity = bounceDirection * wallBounceForce;
        }
    }

}
