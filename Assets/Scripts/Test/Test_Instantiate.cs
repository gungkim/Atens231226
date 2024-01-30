using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Instantiate : TestBase
{
    public GameObject prefab;

#if UNITY_EDITOR
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        new GameObject();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Instantiate(prefab);
    }

    
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Instantiate(prefab, new Vector3(5, 0, 0), Quaternion.identity);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Instantiate(prefab, this.transform);
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
        Debug.Log("시작");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("종료");
    }
#endif
}
