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
    public float WeaponSpeed = 1.0f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }


    public void Attack()
    {       
        
    }
}
