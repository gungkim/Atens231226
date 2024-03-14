using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{   
    
    Transform hinge;

    float attackSpeed = 2.0f;

    InputActions inputActions;

    Rigidbody2D rigid;

    readonly int IsMove_Hash = Animator.StringToHash("IsMove");

    public float rotationAngle = 90f;

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        inputActions = new InputActions();
       
        hinge = transform.GetChild(0);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.OnAttack.performed += OnAttack;
    }

 

    private void OnDisable()
    {
        inputActions.Player.OnAttack.canceled -= OnAttack;
        inputActions.Player.Disable();
    }


    void Start()
    {
        
    }


    void Update()
    {
        Transform hinge = transform.Find("Hinge");
    }

   private void OnAttack(InputAction.CallbackContext context)
    {
        hinge.Rotate(Vector3.down, rotationAngle * Time.deltaTime);
        Debug.Log("공격");
    }
}
