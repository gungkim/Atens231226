using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ���� �θ� Ŭ����
/// </summary>
public class DoorBase : MonoBehaviour
{
    /// <summary>
    /// �� ���� ����(������� ������ null)
    /// </summary>
    public DoorKey key = null;

    Animator animator;

    readonly int IsOpenHash = Animator.StringToHash("IsOpen");

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        if (key != null) // ���谡 ������
        {
            key.onConsume += OnKeyUsed;
        }
    }

    /// <summary>
    /// ���� ���� �� �� �������� ���� ó���ؾ��� ���� ����� �����Լ�
    /// </summary>
    protected virtual void OnOpen()
    {
    }

    /// <summary>
    /// ���� ���� �� �� �������� ���� ó���ؾ��� ���� ����� �����Լ�
    /// </summary>
    protected virtual void OnClose()
    {
    }

    /// <summary>
    /// ����� ���谡 �Һ�� �� �������� ����� �����Լ�
    /// </summary>
    protected virtual void OnKeyUsed()
    {
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    public void Open()
    {
        animator.SetBool(IsOpenHash, true);
        OnOpen();
    }

    /// <summary>
    /// ���� �ݴ� �Լ�
    /// </summary>
    public void Close()
    {
        OnClose();
        animator.SetBool(IsOpenHash, false);
    }
}
