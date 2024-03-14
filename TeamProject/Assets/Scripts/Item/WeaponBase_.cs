using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase_ : MonoBehaviour
{
    //컴포넌트 불러오기
    Rigidbody2D rb;
   
    /// <summary>
    /// 무기의 데미지 
    /// </summary>
    public int weaponDamage = 0;

    /// <summary>
    /// 무기의 공격속도
    /// </summary>
    public float WeaponSpeed = 1;

    public float animationSpeed = 1.0f;
    
    public static Transform currentWeapon;

    Animator animator;

    readonly int NAttack_Hash = Animator.StringToHash("NAttack");
    readonly int HAttack_Hash = Animator.StringToHash("HAttack");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    protected virtual void Start()
    {
        //player = GameManager.Instantiate.Player;
    }

    protected virtual void Update()
    {
        
    }

    private void FixedUpdate()
    {
        animator.speed = animationSpeed * WeaponSpeed;
    }

    /// <summary>
    /// 충돌을 검출하는 메서드
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // 몹과 충돌을 하게되면
        {
            Damaged(1); // 데미지 함수를 실행시킨다.
        }
    }

    //강공격





    //약공격







    /// <summary>
    /// 몹이 피해를 받을 때 실행할 함수 생성
    /// </summary>
    /// <param name="Damage">플레이어가 몹에게 입히는 피해</param>
    /// <exception cref="NotImplementedException"></exception>
    private void Damaged(int Damage)
    {
        
    }
}
