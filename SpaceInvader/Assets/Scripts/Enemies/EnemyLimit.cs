using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLimit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CanvasBehaviour.instance.ShowGameOver();
        }
    }
}
