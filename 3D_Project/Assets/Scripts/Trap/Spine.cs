using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Spine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IAlive live = other.GetComponent<IAlive>();
        if (live != null)
        {
            live.Die(); // 죽을 수 있는 오브젝트는 죽이기
        }
    }
}
