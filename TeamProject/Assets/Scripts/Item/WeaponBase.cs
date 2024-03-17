using Unity.VisualScripting;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Animator animator;
    
    /// <summary>
    /// 무기 공격 데미지
    /// </summary>
    public int weaponDamage = 10;
       
    /// <summary>
    /// 무기 공격 속도
    /// </summary>
    public float weaponSpeed = 3.0f;

    
    bool isAttack = false;

    /// <summary>
    /// 무기판매 가격
    /// </summary>
    public uint price = 0;

    /// <summary>
    /// 무기의 대한 부가 설명
    /// </summary>
    public string description = "설명";

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 무기를 공격할 때 공격속도를 적용
    /// </summary>
    public void Attack()
    {
        if(!isAttack)
        {
            animator.speed = weaponSpeed;
            animator.SetTrigger("Attack");
            isAttack = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 "Enemy" 태그를 가지고 있는지 확인
        if (collision.CompareTag("Enemy"))
        {
            // 충돌한 객체가 Enemy 스크립트를 가지고 있는지 확인
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                // 적에게 공격 데미지를 입힘
                int totalDamage = weaponDamage + Player.Instance.attackPower;
                enemy.Damaged(totalDamage);
            }
        }
    }
}
