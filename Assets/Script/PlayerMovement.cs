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
    private float _maxTime=1f;  // 최대로 눌리는 시간
    private float _pressTime;   // 눌렀을때 시간 
    public GameObject jumpgauge;
    private Animation _jumpgagueAnimation;

    public bool playerIsActive = true;
    private float deadLine = -5;
    public Logic logic;

    private enum MovementState { idle, running, jumping, falling, ready }
    private MovementState _currentState;

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
            // _jumpgagueAnimation.Play();
            Debug.Log(_pressTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DriveJump();
        
        if(playerIsActive)
        {
            dirX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        if(Input.GetKeyUp(KeyCode.Space) && IsGrounded() && playerIsActive) {
            // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // 추후 여기 수정해서 스페이스 누르는 시간에 따라 점프하도록

            jumpgauge.SetActive(false);
            _pressTime = Mathf.Clamp(_pressTime, 0f, _maxTime); // 최소 0초에서 최대 1초 동안 점프 기준을 정함
       
        
            float y = Mathf.Lerp(6f, 10f, _pressTime);
            float x = Mathf.Lerp(1f, 7f, _pressTime);

            Debug.Log(_pressTime);
            Debug.Log(x);
            Debug.Log(y);

            
            // 점프 이벤트
            if (sprite.flipX) // 왼쪽 보고 있을 때 
            {
                rb.velocity = new Vector2(-x, y);
            }
            else // 오른쪽 보고 있을떄
            {
                rb.velocity = new Vector2(x, y);
            }

            _pressTime = 0f;
        }
        
        

        if(transform.position.x < leftWidth){
            transform.position = transform.position + (Vector3.right * 20);
        }else if (transform.position.x > rightWidth){
            transform.position = transform.position + (Vector3.left * 20);
        }

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

        if (dirX > 0f) {
            _currentState = MovementState.running;
            sprite.flipX = false;
        } else if (dirX < 0f) {
            _currentState = MovementState.running;
            sprite.flipX = true;
        } else {
            _currentState = MovementState.idle;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // Debug.Log("Space");
            _currentState = MovementState.ready;
            jumpgauge.SetActive(true);
        }

        if (rb.velocity.y > .1f)
        {
            _currentState = MovementState.jumping;
        } else if (rb.velocity.y < -.1f) {
            _currentState = MovementState.falling;
        }

        // Debug.Log(state);
        anim.SetInteger("state", (int)_currentState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumbleGround);
    }


}
