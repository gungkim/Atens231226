using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;

    Vector3 inputDir = Vector3.zero;

    public float moveSpeed = 0.01f;

    Animator anim;
    readonly int InputX_String = Animator.StringToHash("InputX");
    Rigidbody2D rigid2d;
    SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;

    public GameObject chargedbulletPrefab;

    Transform[] fireTransforms;

    GameObject fireFlash;

    WaitForSeconds flashWait;

    /// <summary>
    /// 연사를 실행할 코루틴
    /// </summary>
    IEnumerator fireCoroutine;

    /// <summary>
    /// 연사 시간 간격
    /// </summary>
    public float fireInterval = 0.5f;

    int score = 0;

    public int Score
    {
        get => score;   
        private set     
        {
            if( score != value)
            {
                score = Math.Min(value,99999);
                onScoreChange?.Invoke(score); 
            }
        }
    }

    /// <summary>
    /// 점수가 변경되었을 때 알리는 델리게이트(파라메터: 변경된 점수)
    /// </summary>
    public Action<int> onScoreChange;

    /// <summary>
    /// 파워 3단계에서 파워업 아이템을 먹었을 때 얻는 보너스 점수
    /// </summary>
    public int powerBonus = 1000;

    /// <summary>
    /// 최소 파워 단계
    /// </summary>
    private const int MinPower = 1;

    /// <summary>
    /// 최대 파워 단계
    /// </summary>
    private const int MaxPower = 3;

    /// <summary>
    /// 한번에 여러 총알을 쏠 때 총알 간의 간격
    /// </summary>
    private const float FireAngle = 30.0f;

    /// <summary>
    /// 현재 파워
    /// </summary>
    private int power = 1;

    /// <summary>
    /// 파워 확인 및 설정용 프로퍼티
    /// </summary>
    private int Power
    {
        get => power;
        set
        {
            if( power != value) // 변경이 있을 때만 처리
            {
                power = value;
                if( power > MaxPower)
                {
                    AddScore(powerBonus);   // 파워가 최대치를 벗어나면 보너스 점수 추가
                }

                power = Mathf.Clamp(power, MinPower, MaxPower); // 파워는 최소~최대 단계로만 존재

                RefreshFirePositions();     // 총알 발사 위치 조정
            }
        }
    }

    private int life = 3;

    const int StartLife = 3;

    private int Life
    {
        get => life;
        set
        {
            if(life != value)
            {
                life = value;
                if(IsAlive)
                {
                    OnHit(); 
                }
                else
                {
                    OnDie(); 
                }

                life = Mathf.Clamp(life, 0, StartLife);
                onLifeChange?.Invoke(life);
            }
        }
    }

    private bool IsAlive => life > 0;

    public Action<int> onLifeChange;

    public float invincibleTime = 2.0f;

    public Action<int> onDie;


    private void Awake()
    {
        inputActions = new PlayerInputActions();        
        anim = GetComponent<Animator>();
        // null; // 참조가 비어있다.
        rigid2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 게임 오브젝트 찾는 방법
        // GameObject.Find("FirePosition"); // 이름으로 게임 오브젝트 찾기
        // GameObject.FindAnyObjectByType<Transform>(); // 특정 컴포넌트를 가지고 있는 게임 오브젝트 찾기
        // GameObject.FindFirstObjectByType<Transform>();  // 특정 컴포넌트를 가지고 있는 첫번째 게임 오브젝트 찾기
        // GameObject.FindGameObjectWithTag("Player");  // 게임 오브젝트의 태그를 기준으로 찾는 함수
        // GameObject.FindGameObjectsWithTag("Player"); // 특정 태그를 가진 모든 게임오브젝트를 찾아주는 함수

        Transform fireRoot = transform.GetChild(0);
        fireTransforms = new Transform[fireRoot.childCount];
        for(int i = 0; i < fireTransforms.Length; i++)
        {
            fireTransforms[i] = fireRoot.GetChild(i);
        }

        fireFlash = transform.GetChild(1).gameObject;
        flashWait = new WaitForSeconds(0.1f);

        fireCoroutine = FireCoroutine();

    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                      
        inputActions.Player.Fire.performed += OnFireStart; 
        inputActions.Player.Fire.canceled += OnFireEnd;    
        inputActions.Player.Boost.performed += OnBoost;
        inputActions.Player.Boost.canceled += OnBoost;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Boost.canceled -= OnBoost;
        inputActions.Player.Boost.performed -= OnBoost;
        inputActions.Player.Fire.canceled -= OnFireEnd;    
        inputActions.Player.Fire.performed -= OnFireStart; 
        inputActions.Player.Disable();                     
    }
    
    private void Start()
    {
        Power = 1;
        Life = StartLife;
    }

    private void FixedUpdate()
    {
        if( IsAlive )
        {
            rigid2d.MovePosition(rigid2d.position + (Vector2)(Time.fixedDeltaTime * moveSpeed * inputDir));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Life--;
        }
        else if( collision.gameObject.CompareTag("PowerUp") )
        {
            Power++;
            collision.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Fire액션이 발동했을 때 실행 시킬 함수
    /// </summary>
    /// <param name="context">입력 관련 정보가 들어있는 구조체 변수</param>
    private void OnFireStart(InputAction.CallbackContext _)
    {
        //if(context.performed)   // 지금 입력이 눌렀다
        //{
        //Debug.Log("OnFireStart : 눌려짐");
        //Fire(fireTransform.position);
        //}
        //if(context.canceled)    // 지금 입력이 떨어졌다
        //{
        //    Debug.Log("OnFireStart : 떨어짐");
        //}        
        StartCoroutine(fireCoroutine);  // 연사시작
    }

    private void OnFireEnd(InputAction.CallbackContext _)
    {
        StopCoroutine(fireCoroutine);   // 연사 중지
    }

    /// <summary>
    /// 연사용 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < Power; i++)
            {
                Fire(fireTransforms[i]);                   // 총알 한발 쏘기
            }
            yield return new WaitForSeconds(fireInterval);  // 인터벌만큼 기다리기
        }
    }

    /// <summary>
    /// 총알을 하나 발사하는 함수
    /// </summary>
    /// <param name="fireTransform">총알이 발사될 트랜스폼</param>
    void Fire(Transform fireTransform)
    {
        //플래시 켜고 끄기
        StartCoroutine(FlashEffect());

        //Instantiate(bulletPrefab, transform); // 발사된 총알도 플레이어의 움직임에 영향을 받는다.
        //Instantiate(bulletPrefab, position, Quaternion.identity);

        Factory.Instance.GetBullet(fireTransform.position, fireTransform.eulerAngles.z);   // 팩토리를 이용해 총알 생성
    }

    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true); 

        yield return flashWait;    

        fireFlash.SetActive(false);
    }

    private void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed)   // 지금 입력이 눌렀다
        {
            Debug.Log("OnBoost : 눌려짐");
        }
        if (context.canceled)    // 지금 입력이 떨어졌다
        {
            Debug.Log("OnBoost : 떨어짐");
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();

        anim.SetFloat(InputX_String, inputDir.x);
    }

    /// <summary>
    /// 점수를 추가해주는 변수
    /// </summary>
    /// <param name="getScore">새로 얻은 점수</param>
    public void AddScore(int getScore)
    {
        Score += getScore;
    }

    /// <summary>
    /// 파워 단계에 따라 총알 발사 위치 조정
    /// </summary>
    private void RefreshFirePositions()
    {
        for (int i = 0; i < MaxPower; i++)
        {
            if (i < Power)   // 파워 단계에 맞게 사용되는 부분 조정
            {
                // 1: 0도
                // 2: +15도, -15도        => 30도 한번
                // 3: +30도, 0도, -30도   => 30도 두번

                float startAngle = (Power - 1) * (FireAngle * 0.5f);    // power에 따라 시작각도를 다르게 설정
                float angleDelta = i * -FireAngle;                      // 30도씩 단계별로 회전
                fireTransforms[i].rotation = Quaternion.Euler(0, 0, startAngle + angleDelta);

                fireTransforms[i].localPosition = Vector3.zero;         // 초기화
                fireTransforms[i].Translate(0.5f, 0.0f, 0.0f);          // 살짝 오른쪽으로 옮기기(로컬 기준)

                fireTransforms[i].gameObject.SetActive(true);           // 활성화
            }
            else
            {
                fireTransforms[i].gameObject.SetActive(false);          // 비활성화
            }
        }
    }

    void OnHit()
    {
        Power--;
        StartCoroutine(InvinvibleMode());
    }


    IEnumerator InvinvibleMode()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");

        float timeElapsed = 0.0f;
        while (timeElapsed < invincibleTime)
        {
            timeElapsed += Time.deltaTime;

            float alpha = (Mathf.Cos(timeElapsed * 30.0f) + 1.0f) * 0.5f;
            spriteRenderer.color = new Color(1, 1, 1, alpha);            

            yield return null;
        }

        gameObject.layer = LayerMask.NameToLayer("Player");
        spriteRenderer.color = Color.white;                

    }

    void OnDie()
    {
        Debug.Log("플레이어가 죽었다.");

        Collider2D body = GetComponent<Collider2D>();
        body.enabled = false;  

        Factory.Instance.GetExplosionEffect(transform.position);

        inputActions.Player.Disable();

        rigid2d.gravityScale = 1.0f;
        rigid2d.freezeRotation = false;
        rigid2d.AddTorque(10000);
        rigid2d.AddForce(Vector2.left * 10.0f, ForceMode2D.Impulse);

        onDie?.Invoke(Score);
    }


#if UNITY_EDITOR
    public void Test_PowerUp()
    {
        Power++;
    }

    public void Test_PowerDown()
    {
        Power--;
    }

    public void Test_Die()
    {
        Life = 0;
    }

    public void Test_SetScore(int score)
    {
        Score = score;
    }
#endif
}
