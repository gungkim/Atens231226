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

    public GameObject ChargingBulletPrefab;

    public GameObject ChargedBulletPrefab;

    Transform[] fireTransforms;

    GameObject fireFlash;

    WaitForSeconds flashWait;

    IEnumerator fireCoroutine;

    public float chargefireInterval = 1.0f;

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


    public Action<int> onScoreChange;


    public int powerBonus = 1000;

    private const int MinPower = 1;

    private const int MaxPower = 3;

    private const float FireAngle = 30.0f;

    private int power = 1;


    private int Power
    {
        get => power;
        set
        {
            if( power != value)
            {
                power = value;
                if( power > MaxPower)
                {
                    AddScore(powerBonus);
                }

                power = Mathf.Clamp(power, MinPower, MaxPower);

                RefreshFirePositions();
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
        rigid2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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


    private void OnFireStart(InputAction.CallbackContext _)
    {
        StartCoroutine(fireCoroutine);
    }

    private void OnFireEnd(InputAction.CallbackContext _)
    {
        StopCoroutine(fireCoroutine);
    }


    IEnumerator FireCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < Power; i++)
            {
                Fire(fireTransforms[i]);


            }
            yield return new WaitForSeconds(chargefireInterval); 
        }
    }


    void Fire(Transform fireTransform)
    {
        StartCoroutine(FlashEffect());

        Factory.Instance.GetBullet(fireTransform.position, fireTransform.eulerAngles.z);
    }

    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true);

        yield return flashWait;

        fireFlash.SetActive(false);
    }


    private void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnBoost : 눌려짐");
        }
        if (context.canceled)
        {
            Debug.Log("OnBoost : 떨어짐");
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();

        anim.SetFloat(InputX_String, inputDir.x);
    }

    public void AddScore(int getScore)
    {
        Score += getScore;
    }

    private void RefreshFirePositions()
    {
        for (int i = 0; i < MaxPower; i++)
        {
            if (i < Power) 
            {

                float startAngle = (Power - 1) * (FireAngle * 0.5f);    
                float angleDelta = i * -FireAngle;                      
                fireTransforms[i].rotation = Quaternion.Euler(0, 0, startAngle + angleDelta);

                fireTransforms[i].localPosition = Vector3.zero;
                fireTransforms[i].Translate(0.5f, 0.0f, 0.0f); 

                fireTransforms[i].gameObject.SetActive(true);  
            }
            else
            {
                fireTransforms[i].gameObject.SetActive(false); 
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

        Factory.Instance.GetHitEffect(transform.position);

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
