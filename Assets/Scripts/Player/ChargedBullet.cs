using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : RecycleObject
{
    // �������ڸ��� ��� ���������� �ʼ� 7�� �����̰� �����

    /// <summary>
    /// �Ѿ��� �̵� �ӵ�
    /// </summary>
    public float moveSpeed = 7.0f;

    /// <summary>
    /// �Ѿ��� ����
    /// </summary>
    public float lifeTime = 10.0f;

    //private void Start()  // Start�� �ѹ��� ����Ǳ� ������ ���� �� �� ������ �ȴ�.
    //{
    //    //Destroy(gameObject, lifeTime);  // lifeTime ���Ŀ� ������ �������
    //    StartCoroutine(LifeOver(lifeTime));
    //}

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(lifeTime));
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.up);    // �� ���� ����? 3��
        //transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);    // �� ���� ����? 4��
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� �ε�ģ �ʿ��� ó��
        //if(collision.gameObject.CompareTag("Wave"))    // �ε�ģ ��밡 Wave �±׸� ������ ������ ����
        //{
        //    Destroy(collision.gameObject);
        //}

        //Instantiate(effectPrefab, transform.position, Quaternion.identity); // hit ����Ʈ ����
        Factory.Instance.GetHitEffect(transform.position);

        //Destroy(gameObject);    // �ڱ� �ڽ��� ������ ����
        gameObject.SetActive(false);    // ��Ȱ��ȭ -> Ǯ�� �ǵ�����
    }
}