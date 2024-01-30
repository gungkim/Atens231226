using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPool : ObjectPool<Boss>
{
    protected override void OnGetObject(Boss component)
    {
        Vector3 pos = component.transform.position;
        pos.y = 0;
        component.transform.position = pos;

        component.OnSpawn();               
    }
}
