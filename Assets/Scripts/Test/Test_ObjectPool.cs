using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_ObjectPool : TestBase
{
    public BulletPool pool1;
    public WavePool pool2;
    public HitEffectPool pool3;

#if UNITY_EDITOR
    private void Start()
    {
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Bullet bullet = pool1.GetObject();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        pool3.GetObject();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        pool2.GetObject();
    }
#endif
}
