using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RecycleObject obj = collision.GetComponent<RecycleObject>();
        if(obj != null)
        {
            collision.gameObject.SetActive(false);
        }
        else
        {
            Destroy(collision.gameObject);
        }

    }
}
