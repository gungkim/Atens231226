using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어의 이동 속도
    public float moveSpeed = 5f;

    // 플레이어의 점프 힘
    public float jumpForce = 5f; // 점프력을 반으로 줄임

    // 플레이어의 Rigidbody2D 컴포넌트
    private Rigidbody2D rb;

    // 플레이어가 땅에 닿아있는지 여부를 나타내는 변수
    private bool isGrounded = false;

    // 플레이어의 점프 중인지 여부를 나타내는 변수
    private bool isJumping = false;

    // 플레이어의 이동 방향을 저장하는 변수
    private float moveDirection = 0f;
    
    // 플레이어의 공격 애니메이터
    private Animator animator;

    // 플레이어의 허용되는 공격 간격
    public float attackCooldown = 0.5f;

    // 마지막으로 공격한 시간
    private float lastAttackTime = 0f;
    
    // 플레이어의 공격력
    public int attackPower = 10;
    
    // 플레이어의 이동 및 공격 상태
    private bool isMoving = false;
    private bool isAttacking = false;

    // 플레이어의 점프 회수를 제한하는 변수
    private int jumpCount = 0;

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameManager.Instance.Player;
            }
            return _instance;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Animator 컴포넌트 찾기
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on Player!");
        }
    }

    void Update()
    {
        // 이동 입력 처리
        moveDirection = Input.GetAxisRaw("Horizontal");

        // 점프 입력 처리
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < 2))
        {
            Jump();
        }

        // 공격 입력 처리
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        // 이동 처리
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿았을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            jumpCount = 0; // 땅에 닿았을 때 점프 회수 초기화
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 땅에서 떨어졌을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCount++; // 점프 회수 증가
    }

    void Attack()
    {
        // 공격 애니메이션 재생
        if (isAttacking)
        {
            animator.SetTrigger("Attack Up"); // 검을 올리는 애니메이션 재생
        }
        else
        {
            animator.SetTrigger("Attack Down"); // 검을 내리는 애니메이션 재생
        }

        // 공격 상태 업데이트
        isAttacking = !isAttacking;

        // 마지막으로 공격한 시간 업데이트
        lastAttackTime = Time.time;
    }
}
