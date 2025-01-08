using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    public float health = 0f;
    [SerializeField]
    public float bulletDamage = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Bullet")
        { 
            if (health <= 0f)
            {
                Destroy(this.gameObject);
            }
            if (health >= 0f)
            {
                health -= bulletDamage;
            }
            Destroy(other.gameObject);
        }
    }
}
