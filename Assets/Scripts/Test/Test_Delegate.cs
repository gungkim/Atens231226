using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Test_Delegate : TestBase
{
    public delegate void TestDelegate1(); 
                                          
    TestDelegate1 aaa;
#if UNITY_EDITOR
    void TestRun1()
    {
        Debug.Log("TestRun1");
    }

    void TestRun2()
    {
        Debug.Log("TestRun2");
    }

    void TestRun3()
    {
        Debug.Log("TestRun3");
    }

    public delegate int TestDelegate2(int a, float b);
    TestDelegate2 bbb;
    int TestRun4(int a, float b)
    {
        return a + (int)b;
    }


    private void Start()
    {
        aaa = TestRun1; 
        aaa += TestRun2;
        aaa = TestRun3 + aaa; 

        bbb = TestRun4;
        
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        aaa();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        aaa = null;
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        aaa?.Invoke();
    }

    
    Action ccc;
    Action<int> ddd;    
    Action<int,int> eee;

    Func<int> f;
    Func<int,float> g;  

    UnityEvent u1;

    void Test_Unity_Del()
    {

    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        u1.AddListener(Test_Unity_Del);        
    }
#endif
}