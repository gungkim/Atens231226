using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Animator animator;
    Player player;
    
    /// <summary>
    /// 무기 공격 데미지
    /// </summary>
    public int weaponDamage = 10;
       
    /// <summary>
    /// 무기 공격 속도
    /// </summary>
    public float weaponSpeed = 1.0f;

    public float critical = 10.0f;

    /// <summary>
    /// 적에게 입히는 데미지의 총합
    /// </summary>
    public int totalDamage =>  weaponDamage + player.attackPower;


    bool isAttack = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameManager.Instance.Player;
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
            if(collision.GetComponent<Enemy>() != null)
            {
                
                // 적에게 공격 데미지를 입힘
                int totalDamage = weaponDamage + player.attackPower;
                enemy.Damaged(totalDamage);
            }
        }
    }
}    // 공격속도 애니메이션 말고 delta time에다가 곱하는걸로 변경. 공격의 모션은 애니메이션이 아닌 일반적 좌표 변환     